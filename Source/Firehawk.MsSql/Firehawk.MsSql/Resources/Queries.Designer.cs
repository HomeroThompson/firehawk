﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fhwk.MsSql.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Queries {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Queries() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Fhwk.MsSql.Resources.Queries", typeof(Queries).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DECLARE @{2} Varchar(255) = (SELECT [name] FROM sysobjects WHERE [xtype] = &apos;PK&apos;AND [parent_obj] = OBJECT_ID(N&apos;[{0}]&apos;)) IF  (@{2} IS NOT NULL) BEGIN Exec sp_rename @{2}, &apos;{1}&apos;, &apos;OBJECT&apos; END.
        /// </summary>
        public static string RenamePrimaryKey {
            get {
                return ResourceManager.GetString("RenamePrimaryKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DECLARE @{3} Varchar(255) = (SELECT &apos;[{0}].[&apos; + [name] + &apos;]&apos; FROM sysobjects WHERE [xtype] = &apos;PK&apos;AND [parent_obj] = OBJECT_ID(N&apos;[{0}].[{1}]&apos;)) IF  (@{3} IS NOT NULL) BEGIN Exec sp_rename @{3}, &apos;{2}&apos;, &apos;OBJECT&apos; END.
        /// </summary>
        public static string RenamePrimaryKeyWithSchema {
            get {
                return ResourceManager.GetString("RenamePrimaryKeyWithSchema", resourceCulture);
            }
        }
    }
}
