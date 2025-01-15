using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x020019A3 RID: 6563
	public class SliceTransformModel : PipelineModel
	{
		// Token: 0x170023B3 RID: 9139
		// (get) Token: 0x0600D68F RID: 54927 RVA: 0x002DA141 File Offset: 0x002D8341
		// (set) Token: 0x0600D690 RID: 54928 RVA: 0x002DA149 File Offset: 0x002D8349
		public PipelineModel EndPosition { get; set; }

		// Token: 0x170023B4 RID: 9140
		// (get) Token: 0x0600D691 RID: 54929 RVA: 0x002DA152 File Offset: 0x002D8352
		public bool IsInfix
		{
			get
			{
				return this.StartPosition != null && this.EndPosition != null;
			}
		}

		// Token: 0x170023B5 RID: 9141
		// (get) Token: 0x0600D692 RID: 54930 RVA: 0x002DA167 File Offset: 0x002D8367
		public bool IsPrefix
		{
			get
			{
				return this.StartPosition == null;
			}
		}

		// Token: 0x170023B6 RID: 9142
		// (get) Token: 0x0600D693 RID: 54931 RVA: 0x002DA172 File Offset: 0x002D8372
		public bool IsSuffix
		{
			get
			{
				return this.EndPosition == null;
			}
		}

		// Token: 0x170023B7 RID: 9143
		// (get) Token: 0x0600D694 RID: 54932 RVA: 0x002DA17D File Offset: 0x002D837D
		// (set) Token: 0x0600D695 RID: 54933 RVA: 0x002DA185 File Offset: 0x002D8385
		public PipelineModel StartPosition { get; set; }

		// Token: 0x0600D696 RID: 54934 RVA: 0x002DA190 File Offset: 0x002D8390
		public override string ToOperatorString()
		{
			List<string> list = new List<string>();
			if (this.StartPosition != null)
			{
				list.Add(this.StartPosition.ToString());
			}
			if (this.EndPosition != null)
			{
				list.Add(this.EndPosition.ToString());
			}
			return ((this.IsInfix || (this.StartPosition == null && this.EndPosition == null)) ? "Slice" : (this.IsPrefix ? "SlicePrefix" : (this.IsSuffix ? "SliceSuffix" : "Slice"))) + "(" + list.ToJoinString(", ") + ")";
		}
	}
}
