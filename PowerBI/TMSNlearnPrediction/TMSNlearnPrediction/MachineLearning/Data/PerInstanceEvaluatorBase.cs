using System;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200001C RID: 28
	public abstract class PerInstanceEvaluatorBase : IRowMapper, ICanSaveModel
	{
		// Token: 0x06000084 RID: 132 RVA: 0x0000403C File Offset: 0x0000223C
		protected PerInstanceEvaluatorBase(IHostEnvironment env, ISchema schema, string scoreCol, string labelCol)
		{
			this._host = env.Register("PerInstanceRowMapper");
			this._scoreCol = scoreCol;
			this._labelCol = labelCol;
			if (!string.IsNullOrEmpty(this._labelCol) && !schema.TryGetColumnIndex(this._labelCol, ref this._labelIndex))
			{
				throw Contracts.Except(this._host, "Did not find the label column '{0}'", new object[] { this._labelCol });
			}
			if (!schema.TryGetColumnIndex(this._scoreCol, ref this._scoreIndex))
			{
				throw Contracts.Except(this._host, "Did not find column '{0}'", new object[] { this._scoreCol });
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000040E8 File Offset: 0x000022E8
		protected PerInstanceEvaluatorBase(ModelLoadContext ctx, IHostEnvironment env, ISchema schema)
		{
			this._host = env.Register("PerInstanceRowMapper");
			this._scoreCol = ctx.LoadNonEmptyString();
			this._labelCol = ctx.LoadStringOrNull();
			if (!string.IsNullOrEmpty(this._labelCol) && !schema.TryGetColumnIndex(this._labelCol, ref this._labelIndex))
			{
				throw Contracts.ExceptDecode(this._host, "Did not find the label column '{0}'", new object[] { this._labelCol });
			}
			if (!schema.TryGetColumnIndex(this._scoreCol, ref this._scoreIndex))
			{
				throw Contracts.ExceptDecode(this._host, "Did not find column '{0}'", new object[] { this._scoreCol });
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000419D File Offset: 0x0000239D
		public virtual void Save(ModelSaveContext ctx)
		{
			ctx.SaveNonEmptyString(this._scoreCol);
			ctx.SaveStringOrNull(this._labelCol);
		}

		// Token: 0x06000087 RID: 135
		public abstract Func<int, bool> GetDependencies(Func<int, bool> activeOutput);

		// Token: 0x06000088 RID: 136
		public abstract RowMapperColumnInfo[] GetOutputColumns();

		// Token: 0x06000089 RID: 137
		public abstract Delegate[] CreateGetters(IRowCursor input, Func<int, bool> activeCols);

		// Token: 0x0400003F RID: 63
		protected readonly IHost _host;

		// Token: 0x04000040 RID: 64
		protected readonly string _scoreCol;

		// Token: 0x04000041 RID: 65
		protected readonly string _labelCol;

		// Token: 0x04000042 RID: 66
		protected readonly int _scoreIndex;

		// Token: 0x04000043 RID: 67
		protected readonly int _labelIndex;
	}
}
