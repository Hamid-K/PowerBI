using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000224 RID: 548
	internal class RdlDataSourcesCollection : IComplexRdlCollection
	{
		// Token: 0x06001279 RID: 4729 RVA: 0x0002980F File Offset: 0x00027A0F
		public IInternalExpression CreateReference()
		{
			return new FunctionDataSourcesCollection();
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x00029816 File Offset: 0x00027A16
		public IInternalExpression CreateReference(IInternalExpression itemNameExpr)
		{
			return new FunctionDataSource(itemNameExpr);
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x00029820 File Offset: 0x00027A20
		public IInternalExpression CreateReference(IInternalExpression itemNameExpr, IInternalExpression propertyNameExpr)
		{
			string constantValue = ((ConstantString)propertyNameExpr).ConstantValue;
			return new FunctionDataSource(itemNameExpr, constantValue);
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00029840 File Offset: 0x00027A40
		public bool IsPredefinedItemProperty(string name)
		{
			return RdlDataSourcesCollection.m_properties.ContainsKey(name);
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x0002984D File Offset: 0x00027A4D
		public bool IsPredefinedItemMethod(string name)
		{
			return false;
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00029850 File Offset: 0x00027A50
		public bool IsPredefinedCollectionProperty(string name)
		{
			return false;
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x00029853 File Offset: 0x00027A53
		public string Name
		{
			get
			{
				return "DataSources";
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x0002985A File Offset: 0x00027A5A
		public string ItemName
		{
			get
			{
				return "DataSource";
			}
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00029861 File Offset: 0x00027A61
		private static Dictionary<string, Type> InitProperties()
		{
			return new Dictionary<string, Type>(StringUtil.CaseInsensitiveComparer)
			{
				{
					"Type",
					typeof(string)
				},
				{
					"DataSourceReference",
					typeof(string)
				}
			};
		}

		// Token: 0x040005D7 RID: 1495
		private static readonly Dictionary<string, Type> m_properties = RdlDataSourcesCollection.InitProperties();
	}
}
