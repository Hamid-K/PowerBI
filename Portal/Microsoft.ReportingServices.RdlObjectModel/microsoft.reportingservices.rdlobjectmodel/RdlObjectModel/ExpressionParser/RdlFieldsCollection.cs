using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000220 RID: 544
	internal class RdlFieldsCollection : IComplexRdlCollection
	{
		// Token: 0x0600124C RID: 4684 RVA: 0x00029489 File Offset: 0x00027689
		public IInternalExpression CreateReference()
		{
			return new FunctionFieldsCollection();
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00029490 File Offset: 0x00027690
		public IInternalExpression CreateReference(IInternalExpression itemNameExpr)
		{
			return new FunctionField(itemNameExpr);
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00029498 File Offset: 0x00027698
		public IInternalExpression CreateReference(IInternalExpression itemNameExpr, IInternalExpression propertyNameExpr)
		{
			return new FunctionField(itemNameExpr, propertyNameExpr);
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x000294A1 File Offset: 0x000276A1
		public bool IsPredefinedItemProperty(string name)
		{
			return RdlFieldsCollection.m_properties.ContainsKey(name);
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x000294AE File Offset: 0x000276AE
		public bool IsPredefinedItemMethod(string name)
		{
			return false;
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x000294B1 File Offset: 0x000276B1
		public bool IsPredefinedCollectionProperty(string name)
		{
			return false;
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x000294B4 File Offset: 0x000276B4
		public string Name
		{
			get
			{
				return "Fields";
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x000294BB File Offset: 0x000276BB
		public string ItemName
		{
			get
			{
				return "Field";
			}
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x000294C4 File Offset: 0x000276C4
		private static Dictionary<string, Type> InitProperties()
		{
			return new Dictionary<string, Type>(StringUtil.CaseInsensitiveComparer)
			{
				{
					"Value",
					typeof(object)
				},
				{
					"IsMissing",
					typeof(bool)
				},
				{
					"UniqueName",
					typeof(string)
				},
				{
					"BackgroundColor",
					typeof(string)
				},
				{
					"Color",
					typeof(string)
				},
				{
					"FontFamily",
					typeof(string)
				},
				{
					"FontSize",
					typeof(string)
				},
				{
					"FontWeight",
					typeof(string)
				},
				{
					"FontStyle",
					typeof(string)
				},
				{
					"TextDecoration",
					typeof(string)
				},
				{
					"FormattedValue",
					typeof(string)
				},
				{
					"Key",
					typeof(object)
				},
				{
					"LevelNumber",
					typeof(int)
				},
				{
					"ParentUniqueName",
					typeof(string)
				}
			};
		}

		// Token: 0x040005D2 RID: 1490
		private static readonly Dictionary<string, Type> m_properties = RdlFieldsCollection.InitProperties();
	}
}
