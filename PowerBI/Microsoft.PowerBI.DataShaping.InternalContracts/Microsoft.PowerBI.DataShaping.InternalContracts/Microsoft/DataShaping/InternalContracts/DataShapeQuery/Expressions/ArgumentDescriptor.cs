using System;
using Microsoft.DataShaping.Common;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions
{
	// Token: 0x020000C2 RID: 194
	internal sealed class ArgumentDescriptor : IEquatable<ArgumentDescriptor>
	{
		// Token: 0x06000508 RID: 1288 RVA: 0x0000A850 File Offset: 0x00008A50
		private ArgumentDescriptor(ExpressionNodeKind? nodeKind, bool isVarArg, ScalarValue? defaultValue)
		{
			Contract.RetailAssert(this.IsValidOptionalArgument(nodeKind, isVarArg, defaultValue), "The arguments descriptor describes an invalid optional argument");
			this.m_nodeKind = nodeKind;
			this.m_isVarArg = isVarArg;
			this.m_defaultValue = defaultValue;
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x0000A880 File Offset: 0x00008A80
		public bool IsVarArg
		{
			get
			{
				return this.m_isVarArg;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0000A888 File Offset: 0x00008A88
		public ScalarValue? DefaultValue
		{
			get
			{
				return this.m_defaultValue;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x0000A890 File Offset: 0x00008A90
		public bool IsOptional
		{
			get
			{
				return this.DefaultValue != null;
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000A8AC File Offset: 0x00008AAC
		public bool MatchesNodeKind(ExpressionNodeKind candidate)
		{
			ExpressionNodeKind? nodeKind = this.m_nodeKind;
			ExpressionNodeKind? expressionNodeKind = ArgumentDescriptor.AnyNodeKind;
			if (!((nodeKind.GetValueOrDefault() == expressionNodeKind.GetValueOrDefault()) & (nodeKind != null == (expressionNodeKind != null))))
			{
				expressionNodeKind = this.m_nodeKind;
				return (candidate == expressionNodeKind.GetValueOrDefault()) & (expressionNodeKind != null);
			}
			return true;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000A904 File Offset: 0x00008B04
		public bool Equals(ArgumentDescriptor other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.m_isVarArg == other.m_isVarArg)
			{
				ExpressionNodeKind? nodeKind = this.m_nodeKind;
				ExpressionNodeKind? nodeKind2 = other.m_nodeKind;
				if ((nodeKind.GetValueOrDefault() == nodeKind2.GetValueOrDefault()) & (nodeKind != null == (nodeKind2 != null)))
				{
					return this.m_defaultValue == other.m_defaultValue;
				}
			}
			return false;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000A998 File Offset: 0x00008B98
		public bool IsValidOptionalArgument(ExpressionNodeKind? nodeKind, bool isVarArg, ScalarValue? defaultValue)
		{
			bool flag = true;
			if (defaultValue != null)
			{
				flag = !isVarArg && nodeKind != null && nodeKind.Value == ExpressionNodeKind.Literal;
			}
			return flag;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000A9CC File Offset: 0x00008BCC
		public static ArgumentDescriptor OfKind(ExpressionNodeKind kind, bool isVarArg = false, ScalarValue? defaultValue = null)
		{
			return new ArgumentDescriptor(new ExpressionNodeKind?(kind), isVarArg, defaultValue);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000A9DC File Offset: 0x00008BDC
		public static ArgumentDescriptor OfAnyKind(bool isVarArg = false)
		{
			return new ArgumentDescriptor(ArgumentDescriptor.AnyNodeKind, isVarArg, null);
		}

		// Token: 0x0400021E RID: 542
		private static readonly ExpressionNodeKind? AnyNodeKind;

		// Token: 0x0400021F RID: 543
		private readonly ExpressionNodeKind? m_nodeKind;

		// Token: 0x04000220 RID: 544
		private readonly bool m_isVarArg;

		// Token: 0x04000221 RID: 545
		private readonly ScalarValue? m_defaultValue;
	}
}
