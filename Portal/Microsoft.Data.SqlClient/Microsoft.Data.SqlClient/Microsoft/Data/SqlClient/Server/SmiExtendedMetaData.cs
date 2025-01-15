using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000121 RID: 289
	internal class SmiExtendedMetaData : SmiMetaData
	{
		// Token: 0x060016C8 RID: 5832 RVA: 0x00061908 File Offset: 0x0005FB08
		internal SmiExtendedMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3)
			: this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, false, null, null, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3)
		{
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x00061934 File Offset: 0x0005FB34
		internal SmiExtendedMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3)
			: this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, null, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3)
		{
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x00061964 File Offset: 0x0005FB64
		internal SmiExtendedMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string udtAssemblyQualifiedName, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3)
			: base(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, udtAssemblyQualifiedName, isMultiValued, fieldMetaData, extendedProperties)
		{
			this._name = name;
			this._typeSpecificNamePart1 = typeSpecificNamePart1;
			this._typeSpecificNamePart2 = typeSpecificNamePart2;
			this._typeSpecificNamePart3 = typeSpecificNamePart3;
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x000619AA File Offset: 0x0005FBAA
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x000619B2 File Offset: 0x0005FBB2
		internal string TypeSpecificNamePart1
		{
			get
			{
				return this._typeSpecificNamePart1;
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x000619BA File Offset: 0x0005FBBA
		internal string TypeSpecificNamePart2
		{
			get
			{
				return this._typeSpecificNamePart2;
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x060016CE RID: 5838 RVA: 0x000619C2 File Offset: 0x0005FBC2
		internal string TypeSpecificNamePart3
		{
			get
			{
				return this._typeSpecificNamePart3;
			}
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x000619CC File Offset: 0x0005FBCC
		internal override string TraceString(int indent)
		{
			return string.Format(CultureInfo.InvariantCulture, "{2}                 Name={0}{1}{2}TypeSpecificNamePart1='{3}'\n\t{2}TypeSpecificNamePart2='{4}'\n\t{2}TypeSpecificNamePart3='{5}'\n\t", new object[]
			{
				this._name ?? "<null>",
				base.TraceString(indent),
				new string(' ', indent),
				this.TypeSpecificNamePart1 ?? "<null>",
				this.TypeSpecificNamePart2 ?? "<null>",
				this.TypeSpecificNamePart3 ?? "<null>"
			});
		}

		// Token: 0x04000946 RID: 2374
		private readonly string _name;

		// Token: 0x04000947 RID: 2375
		private readonly string _typeSpecificNamePart1;

		// Token: 0x04000948 RID: 2376
		private readonly string _typeSpecificNamePart2;

		// Token: 0x04000949 RID: 2377
		private readonly string _typeSpecificNamePart3;
	}
}
