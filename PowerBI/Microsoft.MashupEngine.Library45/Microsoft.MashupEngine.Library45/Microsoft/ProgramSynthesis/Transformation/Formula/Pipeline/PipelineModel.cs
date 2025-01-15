using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x02001986 RID: 6534
	public abstract class PipelineModel
	{
		// Token: 0x17002369 RID: 9065
		// (get) Token: 0x0600D5D6 RID: 54742 RVA: 0x002D9547 File Offset: 0x002D7747
		// (set) Token: 0x0600D5D7 RID: 54743 RVA: 0x002D954F File Offset: 0x002D774F
		public string ColumnName { get; set; }

		// Token: 0x1700236A RID: 9066
		// (get) Token: 0x0600D5D8 RID: 54744 RVA: 0x002D9558 File Offset: 0x002D7758
		// (set) Token: 0x0600D5D9 RID: 54745 RVA: 0x002D9560 File Offset: 0x002D7760
		public DateTimeTransformModel DateTimeTransform { get; set; }

		// Token: 0x1700236B RID: 9067
		// (get) Token: 0x0600D5DA RID: 54746 RVA: 0x002D9569 File Offset: 0x002D7769
		public bool HasDateTimeTransform
		{
			get
			{
				return this.DateTimeTransform != null;
			}
		}

		// Token: 0x1700236C RID: 9068
		// (get) Token: 0x0600D5DB RID: 54747 RVA: 0x002D9574 File Offset: 0x002D7774
		public bool HasNumberTransform
		{
			get
			{
				return this.NumberTransform != null;
			}
		}

		// Token: 0x1700236D RID: 9069
		// (get) Token: 0x0600D5DC RID: 54748 RVA: 0x002D957F File Offset: 0x002D777F
		public bool HasStringTransform
		{
			get
			{
				return this.StringTransform != null;
			}
		}

		// Token: 0x1700236E RID: 9070
		// (get) Token: 0x0600D5DD RID: 54749 RVA: 0x002D958A File Offset: 0x002D778A
		public bool HasTransform
		{
			get
			{
				return this.HasStringTransform || this.HasNumberTransform || this.HasDateTimeTransform;
			}
		}

		// Token: 0x1700236F RID: 9071
		// (get) Token: 0x0600D5DE RID: 54750 RVA: 0x002D95A4 File Offset: 0x002D77A4
		// (set) Token: 0x0600D5DF RID: 54751 RVA: 0x002D95AC File Offset: 0x002D77AC
		public NumberTransformModel NumberTransform { get; set; }

		// Token: 0x17002370 RID: 9072
		// (get) Token: 0x0600D5E0 RID: 54752 RVA: 0x002D95B5 File Offset: 0x002D77B5
		// (set) Token: 0x0600D5E1 RID: 54753 RVA: 0x002D95BD File Offset: 0x002D77BD
		public StringTransformModel StringTransform { get; set; }

		// Token: 0x0600D5E2 RID: 54754
		public abstract string ToOperatorString();

		// Token: 0x0600D5E3 RID: 54755 RVA: 0x002D95C8 File Offset: 0x002D77C8
		public override string ToString()
		{
			if (this._toString != null)
			{
				return this._toString;
			}
			StringTransformModel stringTransform = this.StringTransform;
			string text = ((stringTransform != null) ? stringTransform.ToString() : null);
			NumberTransformModel numberTransform = this.NumberTransform;
			string text2 = ((numberTransform != null) ? numberTransform.ToString() : null);
			DateTimeTransformModel dateTimeTransform = this.DateTimeTransform;
			string text3 = ((dateTimeTransform != null) ? dateTimeTransform.ToString() : null);
			List<string> list = new List<string>();
			if (this is FromDateTimeModel)
			{
				if (!text3.IsNullOrEmpty())
				{
					list.Add(text3);
				}
				if (!text2.IsNullOrEmpty())
				{
					list.Add(text2);
				}
				if (!text.IsNullOrEmpty())
				{
					list.Add(text);
				}
			}
			else if (this is FromNumberModel)
			{
				if (!text2.IsNullOrEmpty())
				{
					list.Add(text2);
				}
				if (!text3.IsNullOrEmpty())
				{
					list.Add(text3);
				}
				if (!text.IsNullOrEmpty())
				{
					list.Add(text);
				}
			}
			else
			{
				if (!text.IsNullOrEmpty())
				{
					list.Add(text);
				}
				if (!text2.IsNullOrEmpty())
				{
					list.Add(text2);
				}
				if (!text3.IsNullOrEmpty())
				{
					list.Add(text3);
				}
			}
			string text4 = ((this is ICompositePipeline) ? Environment.NewLine : " ");
			string text5;
			if (!list.None<string>())
			{
				text5 = this.ToOperatorString() + text4 + "-> " + list.Select((string c) => c.ToString()).ToJoinString(" -> ");
			}
			else
			{
				text5 = this.ToOperatorString();
			}
			return this._toString = text5;
		}

		// Token: 0x040051F5 RID: 20981
		private string _toString;
	}
}
