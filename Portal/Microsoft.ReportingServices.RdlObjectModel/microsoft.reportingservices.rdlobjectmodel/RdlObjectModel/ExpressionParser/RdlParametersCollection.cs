using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000221 RID: 545
	internal class RdlParametersCollection : IComplexRdlCollection
	{
		// Token: 0x06001257 RID: 4695 RVA: 0x00029615 File Offset: 0x00027815
		public IInternalExpression CreateReference()
		{
			return new FunctionParametersCollection();
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x0002961C File Offset: 0x0002781C
		public IInternalExpression CreateReference(IInternalExpression itemNameExpr)
		{
			return new FunctionReportParameter(itemNameExpr);
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x00029624 File Offset: 0x00027824
		public IInternalExpression CreateReference(IInternalExpression itemNameExpr, IInternalExpression propertyNameExpr)
		{
			string constantValue = ((ConstantString)propertyNameExpr).ConstantValue;
			return new FunctionReportParameter(itemNameExpr, constantValue);
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x00029644 File Offset: 0x00027844
		public bool IsPredefinedItemProperty(string name)
		{
			return RdlParametersCollection.m_properties.ContainsKey(name);
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x00029651 File Offset: 0x00027851
		public bool IsPredefinedItemMethod(string name)
		{
			return false;
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00029654 File Offset: 0x00027854
		public bool IsPredefinedCollectionProperty(string name)
		{
			return false;
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x00029657 File Offset: 0x00027857
		public string Name
		{
			get
			{
				return "Parameters";
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x0002965E File Offset: 0x0002785E
		public string ItemName
		{
			get
			{
				return "Parameter";
			}
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00029668 File Offset: 0x00027868
		private static Dictionary<string, Type> InitProperties()
		{
			return new Dictionary<string, Type>(StringUtil.CaseInsensitiveComparer)
			{
				{
					"Value",
					typeof(object)
				},
				{
					"Label",
					typeof(object)
				},
				{
					"Count",
					typeof(int)
				},
				{
					"IsMultiValue",
					typeof(bool)
				}
			};
		}

		// Token: 0x040005D3 RID: 1491
		private static readonly Dictionary<string, Type> m_properties = RdlParametersCollection.InitProperties();
	}
}
