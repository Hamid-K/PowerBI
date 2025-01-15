using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000223 RID: 547
	internal class RdlVariablesCollection : IComplexRdlCollection
	{
		// Token: 0x0600126D RID: 4717 RVA: 0x00029754 File Offset: 0x00027954
		public IInternalExpression CreateReference()
		{
			return new FunctionVariablesCollection();
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x0002975B File Offset: 0x0002795B
		public IInternalExpression CreateReference(IInternalExpression itemNameExpr)
		{
			return new FunctionVariable(itemNameExpr);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00029764 File Offset: 0x00027964
		public IInternalExpression CreateReference(IInternalExpression itemNameExpr, IInternalExpression propertyNameExpr)
		{
			string constantValue = ((ConstantString)propertyNameExpr).ConstantValue;
			return new FunctionVariable(itemNameExpr, constantValue);
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00029784 File Offset: 0x00027984
		public bool IsPredefinedItemProperty(string name)
		{
			return RdlVariablesCollection.m_properties.ContainsKey(name);
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00029791 File Offset: 0x00027991
		public bool IsPredefinedItemMethod(string name)
		{
			return RdlVariablesCollection.m_methods.ContainsKey(name);
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x0002979E File Offset: 0x0002799E
		public bool IsPredefinedCollectionProperty(string name)
		{
			return false;
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x000297A1 File Offset: 0x000279A1
		public string Name
		{
			get
			{
				return "Variables";
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x000297A8 File Offset: 0x000279A8
		public string ItemName
		{
			get
			{
				return "Variable";
			}
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x000297AF File Offset: 0x000279AF
		private static Dictionary<string, Type> InitProperties()
		{
			return new Dictionary<string, Type>(StringUtil.CaseInsensitiveComparer) { 
			{
				"Value",
				typeof(object)
			} };
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x000297D0 File Offset: 0x000279D0
		private static Dictionary<string, Type> InitMethods()
		{
			return new Dictionary<string, Type>(StringUtil.CaseInsensitiveComparer) { 
			{
				"SetValue",
				typeof(object)
			} };
		}

		// Token: 0x040005D5 RID: 1493
		private static readonly Dictionary<string, Type> m_properties = RdlVariablesCollection.InitProperties();

		// Token: 0x040005D6 RID: 1494
		private static readonly Dictionary<string, Type> m_methods = RdlVariablesCollection.InitMethods();
	}
}
