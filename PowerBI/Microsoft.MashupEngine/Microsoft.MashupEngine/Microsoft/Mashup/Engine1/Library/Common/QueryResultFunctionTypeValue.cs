using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200110F RID: 4367
	internal sealed class QueryResultFunctionTypeValue : FunctionTypeValue
	{
		// Token: 0x0600723F RID: 29247 RVA: 0x00189416 File Offset: 0x00187616
		public QueryResultFunctionTypeValue(DbEnvironment environment, string schemaName, string functionName)
		{
			this.environment = environment;
			this.schemaName = schemaName;
			this.functionName = functionName;
		}

		// Token: 0x17001FF7 RID: 8183
		// (get) Token: 0x06007240 RID: 29248 RVA: 0x00189433 File Offset: 0x00187633
		public override TypeValue ReturnType
		{
			get
			{
				if (this.returnType == null)
				{
					this.parameters = this.environment.RetrieveTypeInformationForFunction(this.schemaName, this.functionName, out this.returnType);
				}
				return this.returnType;
			}
		}

		// Token: 0x17001FF8 RID: 8184
		// (get) Token: 0x06007241 RID: 29249 RVA: 0x00189466 File Offset: 0x00187666
		public override RecordValue Parameters
		{
			get
			{
				if (this.parameters == null)
				{
					this.parameters = this.environment.RetrieveTypeInformationForFunction(this.schemaName, this.functionName, out this.returnType);
				}
				return this.parameters;
			}
		}

		// Token: 0x17001FF9 RID: 8185
		// (get) Token: 0x06007242 RID: 29250 RVA: 0x00189499 File Offset: 0x00187699
		public override int Min
		{
			get
			{
				return this.Parameters.Keys.Length;
			}
		}

		// Token: 0x17001FFA RID: 8186
		// (get) Token: 0x06007243 RID: 29251 RVA: 0x00002105 File Offset: 0x00000305
		public override bool Abstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001FFB RID: 8187
		// (get) Token: 0x06007244 RID: 29252 RVA: 0x00002105 File Offset: 0x00000305
		public override bool IsNullable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001FFC RID: 8188
		// (get) Token: 0x06007245 RID: 29253 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override TypeValue NonNullable
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001FFD RID: 8189
		// (get) Token: 0x06007246 RID: 29254 RVA: 0x001894AB File Offset: 0x001876AB
		public override TypeValue Nullable
		{
			get
			{
				if (this.nullableTypeValue == null)
				{
					this.nullableTypeValue = FunctionTypeValue.New(this.ReturnType, this.Parameters, this.Min).Nullable;
				}
				return this.nullableTypeValue;
			}
		}

		// Token: 0x04003F08 RID: 16136
		private readonly DbEnvironment environment;

		// Token: 0x04003F09 RID: 16137
		private readonly string schemaName;

		// Token: 0x04003F0A RID: 16138
		private readonly string functionName;

		// Token: 0x04003F0B RID: 16139
		private RecordValue parameters;

		// Token: 0x04003F0C RID: 16140
		private TypeValue returnType;

		// Token: 0x04003F0D RID: 16141
		private TypeValue nullableTypeValue;
	}
}
