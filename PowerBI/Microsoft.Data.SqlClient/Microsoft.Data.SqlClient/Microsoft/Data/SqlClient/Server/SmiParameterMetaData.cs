using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000122 RID: 290
	internal sealed class SmiParameterMetaData : SmiExtendedMetaData
	{
		// Token: 0x060016D0 RID: 5840 RVA: 0x00061A4C File Offset: 0x0005FC4C
		internal SmiParameterMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, ParameterDirection direction)
			: this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, null, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, direction)
		{
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x00061A7C File Offset: 0x0005FC7C
		internal SmiParameterMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string udtAssemblyQualifiedName, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, ParameterDirection direction)
			: base(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, udtAssemblyQualifiedName, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3)
		{
			this._direction = direction;
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x060016D2 RID: 5842 RVA: 0x00061AB2 File Offset: 0x0005FCB2
		internal ParameterDirection Direction
		{
			get
			{
				return this._direction;
			}
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x00061ABA File Offset: 0x0005FCBA
		internal override string TraceString(int indent)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}            Direction={2:g}\n\t", base.TraceString(indent), new string(' ', indent), this.Direction);
		}

		// Token: 0x0400094A RID: 2378
		private readonly ParameterDirection _direction;
	}
}
