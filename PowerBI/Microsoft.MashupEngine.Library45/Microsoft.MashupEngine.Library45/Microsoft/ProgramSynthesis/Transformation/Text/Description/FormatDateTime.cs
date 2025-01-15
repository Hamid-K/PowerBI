using System;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Description
{
	// Token: 0x02001C89 RID: 7305
	public class FormatDateTime : TransformationDescription
	{
		// Token: 0x0600F759 RID: 63321 RVA: 0x0034B880 File Offset: 0x00349A80
		internal FormatDateTime(FormatPartialDateTime programNode, int partIdx)
			: base(programNode.Node, TransformationCategory.Format, TransformationKind.FormatDateTime)
		{
			this._partIdx = partIdx;
			this.WholeFormat = programNode.outputDtFormat.Value;
			this.Format = this.WholeFormat.FormatParts[partIdx];
		}

		// Token: 0x1700293A RID: 10554
		// (get) Token: 0x0600F75A RID: 63322 RVA: 0x0034B8D3 File Offset: 0x00349AD3
		public DateTimeFormatPart Format { get; }

		// Token: 0x1700293B RID: 10555
		// (get) Token: 0x0600F75B RID: 63323 RVA: 0x0034B8DB File Offset: 0x00349ADB
		[JsonIgnore]
		internal DateTimeFormat WholeFormat { get; }

		// Token: 0x1700293C RID: 10556
		// (get) Token: 0x0600F75C RID: 63324 RVA: 0x0034B8E3 File Offset: 0x00349AE3
		protected override object ExtraIdentity
		{
			get
			{
				return this._partIdx;
			}
		}

		// Token: 0x04005B90 RID: 23440
		private readonly int _partIdx;
	}
}
