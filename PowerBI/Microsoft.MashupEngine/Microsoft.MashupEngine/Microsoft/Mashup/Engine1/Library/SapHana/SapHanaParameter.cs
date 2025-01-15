using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x0200046E RID: 1134
	internal sealed class SapHanaParameter
	{
		// Token: 0x060025AE RID: 9646 RVA: 0x0006CCD0 File Offset: 0x0006AED0
		public SapHanaParameter(SapHanaOdbcDataSource dataSource, SapHanaCubeBase cube, string name, string dataType, SapHanaValueType valueType, SapHanaSelectionType selectionType, bool isMandatory, int order, string description, string placeholderName, string valueAttribute, bool isMultiline, string valueEntity, string descriptionAttribute, List<Value> defaultValues, string attributeSource, string modelElementName)
		{
			this.dataSource = dataSource;
			this.cube = cube;
			this.name = name;
			this.dataTypeName = dataType;
			this.valueType = valueType;
			this.selectionType = selectionType;
			this.isMandatory = isMandatory;
			this.order = order;
			this.description = description;
			this.placeholderName = placeholderName;
			this.valueAttribute = valueAttribute;
			this.isMultiline = isMultiline;
			this.defaultValues = defaultValues;
			this.modelElementName = modelElementName;
			if (valueType == SapHanaValueType.AttributeValue)
			{
				this.valueEntity = valueEntity;
				this.attributeSource = attributeSource;
				this.descriptionAttribute = descriptionAttribute;
			}
		}

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x060025AF RID: 9647 RVA: 0x0006CD6D File Offset: 0x0006AF6D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x060025B0 RID: 9648 RVA: 0x0006CD75 File Offset: 0x0006AF75
		public string DataTypeName
		{
			get
			{
				return this.dataTypeName;
			}
		}

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x060025B1 RID: 9649 RVA: 0x0006CD7D File Offset: 0x0006AF7D
		public SapHanaValueType ValueType
		{
			get
			{
				return this.valueType;
			}
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x060025B2 RID: 9650 RVA: 0x0006CD85 File Offset: 0x0006AF85
		public SapHanaSelectionType SelectionType
		{
			get
			{
				return this.selectionType;
			}
		}

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x060025B3 RID: 9651 RVA: 0x0006CD8D File Offset: 0x0006AF8D
		public bool IsMandatory
		{
			get
			{
				return this.isMandatory;
			}
		}

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x060025B4 RID: 9652 RVA: 0x0006CD95 File Offset: 0x0006AF95
		public int Order
		{
			get
			{
				return this.order;
			}
		}

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x060025B5 RID: 9653 RVA: 0x0006CD9D File Offset: 0x0006AF9D
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x060025B6 RID: 9654 RVA: 0x0006CDA5 File Offset: 0x0006AFA5
		public string PlaceholderName
		{
			get
			{
				return this.placeholderName;
			}
		}

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x060025B7 RID: 9655 RVA: 0x0006CDAD File Offset: 0x0006AFAD
		public string ValueAttribute
		{
			get
			{
				return this.valueAttribute;
			}
		}

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x060025B8 RID: 9656 RVA: 0x0006CDB5 File Offset: 0x0006AFB5
		public string DescriptionAttribute
		{
			get
			{
				return this.descriptionAttribute;
			}
		}

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x060025B9 RID: 9657 RVA: 0x0006CDBD File Offset: 0x0006AFBD
		public List<Value> DefaultValues
		{
			get
			{
				return this.defaultValues;
			}
		}

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x060025BA RID: 9658 RVA: 0x0006CDC5 File Offset: 0x0006AFC5
		public bool IsMultiline
		{
			get
			{
				return this.isMultiline;
			}
		}

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x060025BB RID: 9659 RVA: 0x0006CDCD File Offset: 0x0006AFCD
		public string ModelElementName
		{
			get
			{
				return this.modelElementName;
			}
		}

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x060025BC RID: 9660 RVA: 0x0006CDD5 File Offset: 0x0006AFD5
		public SapHanaParameterKind Kind
		{
			get
			{
				if (string.IsNullOrEmpty(this.placeholderName))
				{
					return SapHanaParameterKind.Variable;
				}
				return SapHanaParameterKind.Parameter;
			}
		}

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x060025BD RID: 9661 RVA: 0x0006CDE8 File Offset: 0x0006AFE8
		public bool HasValues
		{
			get
			{
				SapHanaValueType sapHanaValueType = this.ValueType;
				return sapHanaValueType - SapHanaValueType.StaticList <= 1;
			}
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x0006CE08 File Offset: 0x0006B008
		public IDataReader GetValues()
		{
			SapHanaValueType sapHanaValueType = this.ValueType;
			if (sapHanaValueType == SapHanaValueType.StaticList)
			{
				return this.GetStaticListValues();
			}
			if (sapHanaValueType == SapHanaValueType.AttributeValue)
			{
				return this.GetAttributeValues();
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x0006CE38 File Offset: 0x0006B038
		private IDataReader GetAttributeValues()
		{
			string text = this.dataSource.SqlSettings.QuoteIdentifier(this.ValueAttribute);
			string text2 = ((!string.IsNullOrEmpty(this.attributeSource)) ? this.attributeSource : string.Format(CultureInfo.InvariantCulture, "{0}.{1}", this.dataSource.SqlSettings.QuoteIdentifier(this.cube.SchemaName), this.dataSource.SqlSettings.QuoteIdentifier(this.cube.ViewName)));
			text2 = (text2.StartsWith("\"", StringComparison.Ordinal) ? text2 : this.dataSource.SqlSettings.QuoteIdentifier(text2));
			string text3;
			string text4;
			if (string.IsNullOrEmpty(this.DescriptionAttribute))
			{
				text3 = "NULL";
				text4 = text;
			}
			else if (this.ValueAttribute.Equals(this.DescriptionAttribute, StringComparison.Ordinal))
			{
				text3 = string.Format(CultureInfo.InvariantCulture, "CAST({0} AS NVARCHAR)", text);
				text4 = text;
			}
			else
			{
				text3 = this.dataSource.SqlSettings.QuoteIdentifier(this.DescriptionAttribute);
				text4 = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", text, text3);
			}
			string text5 = string.Format(CultureInfo.InvariantCulture, "select {0} as VALUE, {1} as DESCRIPTION\r\nfrom {2}\r\ngroup by {3}\r\norder by VALUE", new object[] { text, text3, text2, text4 });
			return this.dataSource.Execute(this.dataSource.Host.GetMetadataCache(), text5, null, EmptyArray<OdbcParameter>.Instance, RowRange.All, SapHanaParameter.columnNames, true, null);
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x0006CFB4 File Offset: 0x0006B1B4
		private IDataReader GetStaticListValues()
		{
			string text = "select cast(NAME as NVARCHAR) as VALUE, DESCRIPTION\r\nfrom _SYS_BI.BIMC_VARIABLE_VALUE\r\nwhere CATALOG_NAME = ? and CUBE_NAME = ? and VARIABLE_NAME = ?\r\norder by \"ORDER\"";
			return this.dataSource.Execute(this.dataSource.Host.GetMetadataCache(), text, null, new OdbcParameter[]
			{
				new OdbcParameter(this.cube.CatalogName, OdbcTypeMap.WVarchar),
				new OdbcParameter(this.cube.Name, OdbcTypeMap.WVarchar),
				new OdbcParameter(this.Name, OdbcTypeMap.WVarchar)
			}, RowRange.All, SapHanaParameter.columnNames, true, null);
		}

		// Token: 0x04000FA5 RID: 4005
		private static readonly string[] columnNames = new string[] { "VALUE", "DESCRIPTION" };

		// Token: 0x04000FA6 RID: 4006
		private readonly SapHanaOdbcDataSource dataSource;

		// Token: 0x04000FA7 RID: 4007
		private readonly SapHanaCubeBase cube;

		// Token: 0x04000FA8 RID: 4008
		private readonly string name;

		// Token: 0x04000FA9 RID: 4009
		private readonly string dataTypeName;

		// Token: 0x04000FAA RID: 4010
		private readonly SapHanaValueType valueType;

		// Token: 0x04000FAB RID: 4011
		private readonly SapHanaSelectionType selectionType;

		// Token: 0x04000FAC RID: 4012
		private readonly bool isMandatory;

		// Token: 0x04000FAD RID: 4013
		private readonly int order;

		// Token: 0x04000FAE RID: 4014
		private readonly string description;

		// Token: 0x04000FAF RID: 4015
		private readonly string placeholderName;

		// Token: 0x04000FB0 RID: 4016
		private readonly string valueAttribute;

		// Token: 0x04000FB1 RID: 4017
		private readonly bool isMultiline;

		// Token: 0x04000FB2 RID: 4018
		private readonly string valueEntity;

		// Token: 0x04000FB3 RID: 4019
		private readonly string descriptionAttribute;

		// Token: 0x04000FB4 RID: 4020
		private readonly List<Value> defaultValues;

		// Token: 0x04000FB5 RID: 4021
		private readonly string modelElementName;

		// Token: 0x04000FB6 RID: 4022
		private readonly string attributeSource;
	}
}
