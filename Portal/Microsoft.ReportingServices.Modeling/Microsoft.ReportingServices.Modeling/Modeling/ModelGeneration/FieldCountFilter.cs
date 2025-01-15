using System;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000DF RID: 223
	public sealed class FieldCountFilter : Filter, ITableFilter
	{
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x00026AD8 File Offset: 0x00024CD8
		public NumericCompareExpression CompareExpression
		{
			get
			{
				return this.m_compareExpr;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x00026AE0 File Offset: 0x00024CE0
		// (set) Token: 0x06000BCE RID: 3022 RVA: 0x00026AE8 File Offset: 0x00024CE8
		public Type FieldType
		{
			get
			{
				return this.m_fieldType;
			}
			set
			{
				this.m_fieldType = value;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x00026AF1 File Offset: 0x00024CF1
		// (set) Token: 0x06000BD0 RID: 3024 RVA: 0x00026AF9 File Offset: 0x00024CF9
		public bool? Hidden
		{
			get
			{
				return this.m_hidden;
			}
			set
			{
				this.m_hidden = value;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x00026B02 File Offset: 0x00024D02
		// (set) Token: 0x06000BD2 RID: 3026 RVA: 0x00026B0A File Offset: 0x00024D0A
		public bool? IsAggregate
		{
			get
			{
				return this.m_isAggregate;
			}
			set
			{
				this.m_isAggregate = value;
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00026B14 File Offset: 0x00024D14
		public bool IsMatch(DsvTable table)
		{
			ModelEntity entity = base.BindingContext.GetBindingInfo(table).Entity;
			if (entity == null)
			{
				return false;
			}
			int count = 0;
			ModelItem.VisitAllItems(entity, delegate(ModelItem item)
			{
				if (this.IsItemMatch(item))
				{
					int count2 = count;
					count = count2 + 1;
				}
			});
			return this.m_compareExpr.Evaluate(count);
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00026B74 File Offset: 0x00024D74
		private bool IsItemMatch(ModelItem item)
		{
			if (!(item is ModelField))
			{
				return false;
			}
			if (this.m_fieldType != null && !this.m_fieldType.IsInstanceOfType(item))
			{
				return false;
			}
			if (this.m_hidden != null)
			{
				bool hidden = item.Hidden;
				bool? flag = this.m_hidden;
				if (!((hidden == flag.GetValueOrDefault()) & (flag != null)))
				{
					return false;
				}
			}
			if (this.m_isAggregate != null && item is ModelAttribute)
			{
				bool isAggregate = ((ModelAttribute)item).IsAggregate;
				bool? flag = this.m_isAggregate;
				if (!((isAggregate == flag.GetValueOrDefault()) & (flag != null)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00026C18 File Offset: 0x00024E18
		internal override bool LoadXmlAttribute(ModelingXmlReader xr, IXmlObjectFactory objectFactory)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (!(localName == "count"))
				{
					if (localName == "fieldType")
					{
						FieldCountFilter.XmlFieldType xmlFieldType = xr.ReadValueAsEnum<FieldCountFilter.XmlFieldType>();
						if (xmlFieldType != FieldCountFilter.XmlFieldType.Attribute)
						{
							if (xmlFieldType != FieldCountFilter.XmlFieldType.Role)
							{
								throw new InternalModelingException("Unhandled XmlFieldType " + xmlFieldType.ToString());
							}
							this.m_fieldType = typeof(ModelRole);
						}
						else
						{
							this.m_fieldType = typeof(ModelAttribute);
						}
						return true;
					}
					if (localName == "hidden")
					{
						this.m_hidden = new bool?(xr.ReadValueAsBoolean());
						return true;
					}
					if (localName == "isAggregate")
					{
						this.m_isAggregate = new bool?(xr.ReadValueAsBoolean());
						return true;
					}
				}
				else
				{
					if (!this.m_compareExpr.Parse(xr.ReadValueAsString()))
					{
						throw new RuleConfigurationException("Invalid count expression");
					}
					return true;
				}
			}
			return base.LoadXmlAttribute(xr, objectFactory);
		}

		// Token: 0x040004D9 RID: 1241
		private const string CountAttr = "count";

		// Token: 0x040004DA RID: 1242
		private const string FieldTypeAttr = "fieldType";

		// Token: 0x040004DB RID: 1243
		private const string HiddenAttr = "hidden";

		// Token: 0x040004DC RID: 1244
		private const string IsAggregateAttr = "isAggregate";

		// Token: 0x040004DD RID: 1245
		private readonly NumericCompareExpression m_compareExpr = new NumericCompareExpression();

		// Token: 0x040004DE RID: 1246
		private Type m_fieldType;

		// Token: 0x040004DF RID: 1247
		private bool? m_hidden;

		// Token: 0x040004E0 RID: 1248
		private bool? m_isAggregate;

		// Token: 0x020001C3 RID: 451
		private enum XmlFieldType
		{
			// Token: 0x040007D4 RID: 2004
			Attribute,
			// Token: 0x040007D5 RID: 2005
			Role
		}
	}
}
