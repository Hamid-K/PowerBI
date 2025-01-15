using System;
using System.Globalization;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000F6 RID: 246
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class PropertyFilter : Filter
	{
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x00028F14 File Offset: 0x00027114
		// (set) Token: 0x06000C63 RID: 3171 RVA: 0x00028F1C File Offset: 0x0002711C
		public virtual string Property
		{
			get
			{
				return this.__property;
			}
			set
			{
				this.__property = value;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x00028F25 File Offset: 0x00027125
		// (set) Token: 0x06000C65 RID: 3173 RVA: 0x00028F2D File Offset: 0x0002712D
		public string ExactValue
		{
			get
			{
				return this.m_exactValue;
			}
			set
			{
				this.m_exactValue = value ?? string.Empty;
				this.m_range = null;
				this.m_pattern = null;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00028F4D File Offset: 0x0002714D
		// (set) Token: 0x06000C67 RID: 3175 RVA: 0x00028F55 File Offset: 0x00027155
		public NumericCompareExpression Range
		{
			get
			{
				return this.m_range;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_range = value;
				this.m_exactValue = null;
				this.m_pattern = null;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x00028F7A File Offset: 0x0002717A
		// (set) Token: 0x06000C69 RID: 3177 RVA: 0x00028F84 File Offset: 0x00027184
		public string Pattern
		{
			get
			{
				return this.m_pattern;
			}
			set
			{
				this.m_pattern = value ?? string.Empty;
				this.m_regex = new Regex("^" + this.m_pattern + "$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
				this.m_exactValue = null;
				this.m_range = null;
			}
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x00028FD4 File Offset: 0x000271D4
		public override bool IsMatch(DsvItem dsvItem)
		{
			string propertyValue = this.GetPropertyValue(dsvItem);
			if (this.m_exactValue != null)
			{
				return string.Compare(propertyValue, this.m_exactValue, StringComparison.OrdinalIgnoreCase) == 0;
			}
			if (this.m_range != null)
			{
				decimal num;
				if (propertyValue.Length == 0)
				{
					num = 0m;
				}
				else
				{
					try
					{
						num = Convert.ToDecimal(propertyValue, CultureInfo.InvariantCulture);
					}
					catch (FormatException)
					{
						return false;
					}
				}
				return this.m_range.Evaluate(num);
			}
			return this.m_pattern != null && this.m_regex.IsMatch(propertyValue);
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00029068 File Offset: 0x00027268
		private string GetPropertyValue(DsvItem dsvItem)
		{
			if (this.Property == "DsvItemType")
			{
				if (dsvItem is DsvTable)
				{
					return ((DsvTable)dsvItem).TableType.ToString();
				}
				if (dsvItem is DsvColumn)
				{
					return "Column";
				}
				if (dsvItem is DsvRelation)
				{
					return "Relation";
				}
			}
			DsvColumn dsvColumn = dsvItem as DsvColumn;
			if (dsvColumn != null)
			{
				string property = this.Property;
				if (property == "DataType")
				{
					return dsvColumn.ModelingDataType.GetValueOrDefault(DataType.Null).ToString();
				}
				if (property == "DsvDataType")
				{
					return dsvColumn.DataType.ToString();
				}
			}
			return dsvItem.GetPropertyValue(this.Property) ?? string.Empty;
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x00029134 File Offset: 0x00027334
		protected override bool ShouldCombineWith(Filter other)
		{
			PropertyFilter propertyFilter = other as PropertyFilter;
			return propertyFilter != null && this.Property == propertyFilter.Property;
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00029160 File Offset: 0x00027360
		internal override bool LoadXmlAttribute(ModelingXmlReader xr, IXmlObjectFactory objectFactory)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName == "property")
				{
					this.Property = xr.ReadValueAsString();
					return true;
				}
				if (localName == "value")
				{
					this.ExactValue = xr.ReadValueAsString();
					return true;
				}
				if (!(localName == "range"))
				{
					if (localName == "pattern")
					{
						this.Pattern = xr.ReadValueAsString();
						return true;
					}
				}
				else
				{
					this.Range = new NumericCompareExpression();
					if (!this.Range.Parse(xr.ReadValueAsString()))
					{
						throw new RuleConfigurationException("Invalid range expression");
					}
					return true;
				}
			}
			return base.LoadXmlAttribute(xr, objectFactory);
		}

		// Token: 0x04000516 RID: 1302
		internal const string DsvItemTypeProperty = "DsvItemType";

		// Token: 0x04000517 RID: 1303
		private const string DataTypeProperty = "DataType";

		// Token: 0x04000518 RID: 1304
		private const string DsvDataTypeProperty = "DsvDataType";

		// Token: 0x04000519 RID: 1305
		private const string PropertyAttr = "property";

		// Token: 0x0400051A RID: 1306
		private const string ValueAttr = "value";

		// Token: 0x0400051B RID: 1307
		private const string RangeAttr = "range";

		// Token: 0x0400051C RID: 1308
		private const string PatternAttr = "pattern";

		// Token: 0x0400051D RID: 1309
		private string __property = string.Empty;

		// Token: 0x0400051E RID: 1310
		private string m_exactValue = string.Empty;

		// Token: 0x0400051F RID: 1311
		private NumericCompareExpression m_range;

		// Token: 0x04000520 RID: 1312
		private string m_pattern;

		// Token: 0x04000521 RID: 1313
		private Regex m_regex;
	}
}
