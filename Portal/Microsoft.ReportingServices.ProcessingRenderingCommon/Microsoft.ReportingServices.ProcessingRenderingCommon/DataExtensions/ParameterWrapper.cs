using System;
using System.Data;
using Microsoft.ReportingServices.DataProcessing;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x0200001D RID: 29
	public class ParameterWrapper : BaseDataWrapper, Microsoft.ReportingServices.DataProcessing.IDataParameter
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00004F49 File Offset: 0x00003149
		protected internal ParameterWrapper(global::System.Data.IDataParameter param)
			: base(param)
		{
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004F52 File Offset: 0x00003152
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00004F5F File Offset: 0x0000315F
		public virtual string ParameterName
		{
			get
			{
				return this.UnderlyingParameter.ParameterName;
			}
			set
			{
				this.UnderlyingParameter.ParameterName = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004F6D File Offset: 0x0000316D
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00004F7A File Offset: 0x0000317A
		public virtual object Value
		{
			get
			{
				return this.UnderlyingParameter.Value;
			}
			set
			{
				this.UnderlyingParameter.Value = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004F88 File Offset: 0x00003188
		protected internal global::System.Data.IDataParameter UnderlyingParameter
		{
			get
			{
				return (global::System.Data.IDataParameter)base.UnderlyingObject;
			}
		}
	}
}
