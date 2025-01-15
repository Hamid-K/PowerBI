using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000CE3 RID: 3299
	internal sealed class InvocationCubeExpression : CubeExpression
	{
		// Token: 0x0600597E RID: 22910 RVA: 0x00139660 File Offset: 0x00137860
		public InvocationCubeExpression(CubeExpression function, params CubeExpression[] arguments)
		{
			this.function = function;
			this.arguments = arguments;
		}

		// Token: 0x17001AC2 RID: 6850
		// (get) Token: 0x0600597F RID: 22911 RVA: 0x0000244F File Offset: 0x0000064F
		public override CubeExpressionKind Kind
		{
			get
			{
				return CubeExpressionKind.Invocation;
			}
		}

		// Token: 0x17001AC3 RID: 6851
		// (get) Token: 0x06005980 RID: 22912 RVA: 0x00139676 File Offset: 0x00137876
		public CubeExpression Function
		{
			get
			{
				return this.function;
			}
		}

		// Token: 0x17001AC4 RID: 6852
		// (get) Token: 0x06005981 RID: 22913 RVA: 0x0013967E File Offset: 0x0013787E
		public IList<CubeExpression> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x06005982 RID: 22914 RVA: 0x00139688 File Offset: 0x00137888
		public bool Equals(InvocationCubeExpression other)
		{
			bool flag = other != null && this.function == other.function && this.arguments.Count == other.arguments.Count;
			int num = 0;
			while (flag && num < this.arguments.Count)
			{
				flag &= this.arguments[num].Equals(other.arguments[num]);
				num++;
			}
			return flag;
		}

		// Token: 0x06005983 RID: 22915 RVA: 0x001396FC File Offset: 0x001378FC
		public override bool Equals(object other)
		{
			return this.Equals(other as InvocationCubeExpression);
		}

		// Token: 0x06005984 RID: 22916 RVA: 0x0013970C File Offset: 0x0013790C
		public override int GetHashCode()
		{
			int num = this.function.GetHashCode() + 5011 * this.arguments.Count;
			for (int i = 0; i < this.arguments.Count; i++)
			{
				num += this.arguments[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x0400321C RID: 12828
		private readonly CubeExpression function;

		// Token: 0x0400321D RID: 12829
		private readonly IList<CubeExpression> arguments;
	}
}
