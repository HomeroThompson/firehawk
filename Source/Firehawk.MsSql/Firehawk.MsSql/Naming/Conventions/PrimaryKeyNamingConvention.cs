
namespace Fhwk.Core
{
    /// <summary>
    /// Defines the different naming conventions used when creating the PK names
    /// </summary>
    public enum PrimaryKeyNamingConvention
    {
        /// <summary>
        /// The default NH MCC convention
        /// </summary>
        Default,

        /// <summary>
        /// Custom user defined convention
        /// </summary>
        Custom,

        /// <summary>
        /// PK + TableName + ColumnName
        /// </summary>
        PK_TableName_ColumnName,

        /// <summary>
        /// PK + TableName
        /// </summary>
        PK_TableName,

        /// <summary>
        /// TableName + ColumnName + PK
        /// </summary>
        TableName_ColumnName_PK,

        /// <summary>
        /// TableName + PK
        /// </summary>
        TableName_PK,
    }

}
