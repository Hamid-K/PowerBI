using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Description
{
	// Token: 0x02001C98 RID: 7320
	public abstract class TransformationDescription : IEquatable<TransformationDescription>
	{
		// Token: 0x0600F790 RID: 63376 RVA: 0x0034BF41 File Offset: 0x0034A141
		internal TransformationDescription(ProgramNode programNode, string columnName, TransformationCategory category, TransformationKind kind)
		{
			this.ProgramNode = programNode;
			this.ColumnName = columnName;
			this.Category = category;
			this.Kind = kind;
		}

		// Token: 0x0600F791 RID: 63377 RVA: 0x0034BF66 File Offset: 0x0034A166
		internal TransformationDescription(ProgramNode programNode, TransformationCategory category, TransformationKind kind)
			: this(programNode, null, category, kind)
		{
		}

		// Token: 0x17002959 RID: 10585
		// (get) Token: 0x0600F792 RID: 63378 RVA: 0x0034BF72 File Offset: 0x0034A172
		// (set) Token: 0x0600F793 RID: 63379 RVA: 0x0034BF7A File Offset: 0x0034A17A
		[JsonIgnore]
		internal int? Index { get; set; }

		// Token: 0x1700295A RID: 10586
		// (get) Token: 0x0600F794 RID: 63380 RVA: 0x0034BF83 File Offset: 0x0034A183
		[JsonIgnore]
		public ProgramNode ProgramNode { get; }

		// Token: 0x1700295B RID: 10587
		// (get) Token: 0x0600F795 RID: 63381 RVA: 0x0034BF8B File Offset: 0x0034A18B
		public string ColumnName { get; }

		// Token: 0x1700295C RID: 10588
		// (get) Token: 0x0600F796 RID: 63382 RVA: 0x0034BF93 File Offset: 0x0034A193
		public TransformationCategory Category { get; }

		// Token: 0x1700295D RID: 10589
		// (get) Token: 0x0600F797 RID: 63383 RVA: 0x0034BF9B File Offset: 0x0034A19B
		public TransformationKind Kind { get; }

		// Token: 0x1700295E RID: 10590
		// (get) Token: 0x0600F798 RID: 63384 RVA: 0x00002188 File Offset: 0x00000388
		[JsonIgnore]
		protected virtual object ExtraIdentity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700295F RID: 10591
		// (get) Token: 0x0600F799 RID: 63385 RVA: 0x0034BFA3 File Offset: 0x0034A1A3
		[JsonIgnore]
		internal Record<ProgramNode, object> Identity
		{
			get
			{
				return Record.Create<ProgramNode, object>(this.ProgramNode, this.ExtraIdentity);
			}
		}

		// Token: 0x0600F79A RID: 63386 RVA: 0x0034BFB6 File Offset: 0x0034A1B6
		public bool Equals(TransformationDescription other)
		{
			return other != null && (this == other || object.Equals(this.Identity, other.Identity));
		}

		// Token: 0x0600F79B RID: 63387 RVA: 0x0034BFDE File Offset: 0x0034A1DE
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((TransformationDescription)obj)));
		}

		// Token: 0x0600F79C RID: 63388 RVA: 0x0034C00C File Offset: 0x0034A20C
		public override int GetHashCode()
		{
			return this.Identity.GetHashCode();
		}
	}
}
