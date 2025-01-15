using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200001A RID: 26
	public abstract class RowToRowEvaluatorBase : EvaluatorBase
	{
		// Token: 0x0600007E RID: 126 RVA: 0x00004005 File Offset: 0x00002205
		protected RowToRowEvaluatorBase(IHostEnvironment env, string registrationName)
			: base(env, registrationName)
		{
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004010 File Offset: 0x00002210
		public override IDataTransform GetPerInstanceMetrics(RoleMappedData data)
		{
			IRowMapper rowMapper = this.CreatePerInstanceRowMapper(data.Schema);
			return new RowToRowMapperTransform(this._host, data.Data, rowMapper);
		}

		// Token: 0x06000080 RID: 128
		protected abstract IRowMapper CreatePerInstanceRowMapper(RoleMappedSchema schema);
	}
}
