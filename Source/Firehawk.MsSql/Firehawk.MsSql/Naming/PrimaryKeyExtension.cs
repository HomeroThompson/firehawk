﻿using Fhwk.Core;
using Fhwk.Core.Utils.ConfigExt;
using Fhwk.MsSql.Resources;
using Fhwk.MsSql.Utils.Extensions;
using NHibernate.Cfg.MappingSchema;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace Fhwk.MsSql.Naming
{
    /// <summary>
    /// This extension renames the default database primary key names with names generated by conventions
    /// </summary>
    internal class PrimaryKeyExtension: FirehawkExtension
    {
        #region Public Properties

        /// <summary>
        /// Gets the selected naming convention for primary key names
        /// </summary>
        public PrimaryKeyNamingConvention PrimaryKeyNamingConvention { get; set; }

        /// <summary>
        /// Gets the selected custom naming convention for primary key names
        /// </summary>
        public Func<Type, MemberInfo, string> PrimaryKeyCustomNamingConvention { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Renames the default database primary key names with names generated by conventions.
        /// </summary>
        /// <param name="entityTypes">The list of entities</param>
        public override void AfterCompileMappings(IList<Type> entityTypes, HbmMapping mapping)
        {
            foreach (var entityType in entityTypes)
            {
                string schemaName = NamingEngine.ToSchemaName(entityType) ?? GetSchemaName(mapping, entityType);
                string tableName = NamingEngine.ToTableName(entityType);
                
                var idProperty = ModelMapper.ModelInspector.GetIdentifierMember(entityType);
                string primaryKeyName = ToPrimaryKeyName(entityType, idProperty);
                string varName = "pk" + tableName;

                var query = string.IsNullOrEmpty(schemaName) 
                    ? string.Format(Queries.RenamePrimaryKey, tableName, primaryKeyName, varName)
                    : string.Format(Queries.RenamePrimaryKeyWithSchema, schemaName, tableName, primaryKeyName, varName);

                var obj = new NHibernate.Mapping.SimpleAuxiliaryDatabaseObject(query, null);

                NhConfiguration.AddAuxiliaryDatabaseObject(obj);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Generates the name of a Primary Key constraint from a given entity type and Id property according to the naming conventions configuration
        /// </summary>
        /// <param name="entityType">The entity type</param>
        /// <param name="idProperty">The member info of the Id property</param>
        /// <returns>The primary key name</returns>
        private string ToPrimaryKeyName(Type entityType, MemberInfo idProperty)
        {
            string result = null;
            switch (this.PrimaryKeyNamingConvention)
            {
                case PrimaryKeyNamingConvention.Default: result = idProperty.Name; break;
                case PrimaryKeyNamingConvention.PK_TableName: result = NamingEngine.ToConstraintName(string.Format(ConventionFormats.PrimaryKeyTable, entityType.Name)); break;
                case PrimaryKeyNamingConvention.PK_TableName_ColumnName: result = NamingEngine.ToConstraintName(string.Format(ConventionFormats.PrimaryKeyTableColumn, entityType.Name, idProperty.Name)); break;
                case PrimaryKeyNamingConvention.TableName_ColumnName_PK: result = NamingEngine.ToConstraintName(string.Format(ConventionFormats.PrimaryKeyTableColumnSuffix, entityType.Name, idProperty.Name)); break;
                case PrimaryKeyNamingConvention.TableName_PK: result = NamingEngine.ToConstraintName(string.Format(ConventionFormats.PrimaryKeyTableSuffix, entityType.Name)); break;
                case PrimaryKeyNamingConvention.Custom:
                    {
                        if (this.PrimaryKeyCustomNamingConvention == null)
                            throw new ConfigurationErrorsException(string.Format(ExceptionMessages.CustomConventionNotFound, typeof(PrimaryKeyNamingConvention).Name));

                        result = this.PrimaryKeyCustomNamingConvention(entityType, idProperty);
                    }; break;
            }

            return result;
        }

        /// <summary>
        /// Gets the name of the schema for a given entity type
        /// </summary>
        /// <returns>The schema's name</returns>
        private string GetSchemaName(HbmMapping mapping, Type entityType)
        {
            return mapping.Items.Where(m => m is HbmClass)
                .OfType<HbmClass>()
                .Where(m => m.name == entityType.FullName || m.name == entityType.Name)
                .Select(m => m.schema)
                .SingleOrDefault();
        }
        
        #endregion
    }
}
