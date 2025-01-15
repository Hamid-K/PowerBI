using System;
using Microsoft.HostIntegration.StaticSqlUtil;

namespace Microsoft.HostIntegration.DrdaClient.Packages
{
	// Token: 0x02000A5F RID: 2655
	public class Parameter
	{
		// Token: 0x060052D7 RID: 21207 RVA: 0x00150771 File Offset: 0x0014E971
		internal Parameter(Parameter parameter)
		{
			this._parameter = parameter;
		}

		// Token: 0x060052D8 RID: 21208 RVA: 0x00150780 File Offset: 0x0014E980
		public Parameter()
		{
			this._parameter = new Parameter();
		}

		// Token: 0x17001409 RID: 5129
		// (get) Token: 0x060052D9 RID: 21209 RVA: 0x00150793 File Offset: 0x0014E993
		// (set) Token: 0x060052DA RID: 21210 RVA: 0x001507A0 File Offset: 0x0014E9A0
		public string Name
		{
			get
			{
				return this._parameter.Name;
			}
			set
			{
				this._parameter.Name = value;
			}
		}

		// Token: 0x1700140A RID: 5130
		// (get) Token: 0x060052DB RID: 21211 RVA: 0x001507AE File Offset: 0x0014E9AE
		// (set) Token: 0x060052DC RID: 21212 RVA: 0x001507BB File Offset: 0x0014E9BB
		public ParameterTypes Type
		{
			get
			{
				return (ParameterTypes)this._parameter.Type;
			}
			set
			{
				this._parameter.Type = (ParameterTypes)value;
			}
		}

		// Token: 0x1700140B RID: 5131
		// (get) Token: 0x060052DD RID: 21213 RVA: 0x001507C9 File Offset: 0x0014E9C9
		// (set) Token: 0x060052DE RID: 21214 RVA: 0x001507D6 File Offset: 0x0014E9D6
		public short Ccsid
		{
			get
			{
				return this._parameter.Ccsid;
			}
			set
			{
				this._parameter.Ccsid = value;
			}
		}

		// Token: 0x1700140C RID: 5132
		// (get) Token: 0x060052DF RID: 21215 RVA: 0x001507E4 File Offset: 0x0014E9E4
		// (set) Token: 0x060052E0 RID: 21216 RVA: 0x001507F1 File Offset: 0x0014E9F1
		public short Length
		{
			get
			{
				return this._parameter.Length;
			}
			set
			{
				this._parameter.Length = value;
			}
		}

		// Token: 0x1700140D RID: 5133
		// (get) Token: 0x060052E1 RID: 21217 RVA: 0x001507FF File Offset: 0x0014E9FF
		// (set) Token: 0x060052E2 RID: 21218 RVA: 0x0015080C File Offset: 0x0014EA0C
		public short Scale
		{
			get
			{
				return this._parameter.Scale;
			}
			set
			{
				this._parameter.Scale = value;
			}
		}

		// Token: 0x1700140E RID: 5134
		// (get) Token: 0x060052E3 RID: 21219 RVA: 0x0015081A File Offset: 0x0014EA1A
		// (set) Token: 0x060052E4 RID: 21220 RVA: 0x00150827 File Offset: 0x0014EA27
		public short Precision
		{
			get
			{
				return this._parameter.Precision;
			}
			set
			{
				this._parameter.Precision = value;
			}
		}

		// Token: 0x1700140F RID: 5135
		// (get) Token: 0x060052E5 RID: 21221 RVA: 0x00150835 File Offset: 0x0014EA35
		// (set) Token: 0x060052E6 RID: 21222 RVA: 0x00150842 File Offset: 0x0014EA42
		public bool Nullable
		{
			get
			{
				return this._parameter.Nullable;
			}
			set
			{
				this._parameter.Nullable = value;
			}
		}

		// Token: 0x060052E7 RID: 21223 RVA: 0x00150850 File Offset: 0x0014EA50
		internal Parameter ToParameter()
		{
			return this._parameter;
		}

		// Token: 0x04004156 RID: 16726
		private Parameter _parameter;
	}
}
