using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000225 RID: 549
	internal class RdlDataSetsCollection : IComplexRdlCollection
	{
		// Token: 0x06001284 RID: 4740 RVA: 0x000298AB File Offset: 0x00027AAB
		public IInternalExpression CreateReference()
		{
			return new FunctionDataSetsCollection();
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x000298B2 File Offset: 0x00027AB2
		public IInternalExpression CreateReference(IInternalExpression itemNameExpr)
		{
			return new FunctionDataSet(itemNameExpr);
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x000298BC File Offset: 0x00027ABC
		public IInternalExpression CreateReference(IInternalExpression itemNameExpr, IInternalExpression propertyNameExpr)
		{
			string constantValue = ((ConstantString)propertyNameExpr).ConstantValue;
			return new FunctionDataSet(itemNameExpr, constantValue);
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x000298DC File Offset: 0x00027ADC
		public bool IsPredefinedItemProperty(string name)
		{
			return RdlDataSetsCollection.m_properties.ContainsKey(name);
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x000298E9 File Offset: 0x00027AE9
		public bool IsPredefinedItemMethod(string name)
		{
			return false;
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x000298EC File Offset: 0x00027AEC
		public bool IsPredefinedCollectionProperty(string name)
		{
			return false;
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x0600128A RID: 4746 RVA: 0x000298EF File Offset: 0x00027AEF
		public string Name
		{
			get
			{
				return "DataSets";
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x000298F6 File Offset: 0x00027AF6
		public string ItemName
		{
			get
			{
				return "DataSet";
			}
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x00029900 File Offset: 0x00027B00
		private static Dictionary<string, Type> InitProperties()
		{
			return new Dictionary<string, Type>(StringUtil.CaseInsensitiveComparer)
			{
				{
					"CommandText",
					typeof(string)
				},
				{
					"RewrittenCommandText",
					typeof(string)
				},
				{
					"ExecutionTime",
					typeof(DateTime)
				}
			};
		}

		// Token: 0x040005D8 RID: 1496
		private static readonly Dictionary<string, Type> m_properties = RdlDataSetsCollection.InitProperties();
	}
}
