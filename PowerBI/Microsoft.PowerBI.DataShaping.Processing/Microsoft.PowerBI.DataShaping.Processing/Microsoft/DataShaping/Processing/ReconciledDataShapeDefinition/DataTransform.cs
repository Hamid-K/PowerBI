using System;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000038 RID: 56
	internal sealed class DataTransform
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x00005A61 File Offset: 0x00003C61
		internal DataTransform(string id, string algorithm, DataTransformInput input, DataTransformOutput output)
		{
			this._id = id;
			this._algorithm = algorithm;
			this._input = input;
			this._output = output;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00005A86 File Offset: 0x00003C86
		internal string Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00005A8E File Offset: 0x00003C8E
		internal string Algorithm
		{
			get
			{
				return this._algorithm;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00005A96 File Offset: 0x00003C96
		internal DataTransformInput Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00005A9E File Offset: 0x00003C9E
		internal DataTransformOutput Output
		{
			get
			{
				return this._output;
			}
		}

		// Token: 0x04000102 RID: 258
		private readonly string _id;

		// Token: 0x04000103 RID: 259
		private readonly string _algorithm;

		// Token: 0x04000104 RID: 260
		private readonly DataTransformInput _input;

		// Token: 0x04000105 RID: 261
		private readonly DataTransformOutput _output;
	}
}
