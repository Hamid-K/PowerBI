using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000048 RID: 72
	public abstract class ODataPathSegment : ODataAnnotatable
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x0000785F File Offset: 0x00005A5F
		internal ODataPathSegment(ODataPathSegment other)
		{
			this.CopyValuesFrom(other);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000786E File Offset: 0x00005A6E
		protected ODataPathSegment()
		{
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001D4 RID: 468
		public abstract IEdmType EdmType { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00007876 File Offset: 0x00005A76
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x0000787E File Offset: 0x00005A7E
		internal string Identifier
		{
			get
			{
				return this.identifier;
			}
			set
			{
				this.identifier = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00007887 File Offset: 0x00005A87
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x0000788F File Offset: 0x00005A8F
		internal bool SingleResult
		{
			get
			{
				return this.singleResult;
			}
			set
			{
				this.singleResult = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00007898 File Offset: 0x00005A98
		// (set) Token: 0x060001DA RID: 474 RVA: 0x000078A0 File Offset: 0x00005AA0
		internal IEdmEntitySet TargetEdmEntitySet
		{
			get
			{
				return this.targetEdmEntitySet;
			}
			set
			{
				this.targetEdmEntitySet = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000078A9 File Offset: 0x00005AA9
		// (set) Token: 0x060001DC RID: 476 RVA: 0x000078B1 File Offset: 0x00005AB1
		internal IEdmType TargetEdmType
		{
			get
			{
				return this.targetEdmType;
			}
			set
			{
				this.targetEdmType = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001DD RID: 477 RVA: 0x000078BA File Offset: 0x00005ABA
		// (set) Token: 0x060001DE RID: 478 RVA: 0x000078C2 File Offset: 0x00005AC2
		internal RequestTargetKind TargetKind
		{
			get
			{
				return this.targetKind;
			}
			set
			{
				this.targetKind = value;
			}
		}

		// Token: 0x060001DF RID: 479
		public abstract T Translate<T>(PathSegmentTranslator<T> translator);

		// Token: 0x060001E0 RID: 480
		public abstract void Handle(PathSegmentHandler handler);

		// Token: 0x060001E1 RID: 481 RVA: 0x000078CB File Offset: 0x00005ACB
		internal virtual bool Equals(ODataPathSegment other)
		{
			return object.ReferenceEquals(this, other);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000078D4 File Offset: 0x00005AD4
		internal void CopyValuesFrom(ODataPathSegment other)
		{
			this.Identifier = other.Identifier;
			this.SingleResult = other.SingleResult;
			this.TargetEdmEntitySet = other.TargetEdmEntitySet;
			this.TargetKind = other.TargetKind;
			this.TargetEdmType = other.TargetEdmType;
		}

		// Token: 0x0400007B RID: 123
		private string identifier;

		// Token: 0x0400007C RID: 124
		private bool singleResult;

		// Token: 0x0400007D RID: 125
		private IEdmEntitySet targetEdmEntitySet;

		// Token: 0x0400007E RID: 126
		private IEdmType targetEdmType;

		// Token: 0x0400007F RID: 127
		private RequestTargetKind targetKind;
	}
}
