using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200001A RID: 26
	public struct DocumentRangePair : IEquatable<DocumentRangePair>
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00002C66 File Offset: 0x00000E66
		public DocumentRangePair(DocumentRange dependencyRange, DocumentRange referencedRange)
		{
			this.dependencyRange = dependencyRange;
			this.referencedRange = referencedRange;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002C76 File Offset: 0x00000E76
		public DocumentRange DependencyRange
		{
			get
			{
				return this.dependencyRange;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002C7E File Offset: 0x00000E7E
		public DocumentRange ReferencedRange
		{
			get
			{
				return this.referencedRange;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002C86 File Offset: 0x00000E86
		public override bool Equals(object obj)
		{
			return obj is DocumentRangePair && this.Equals((DocumentRangePair)obj);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002C9E File Offset: 0x00000E9E
		public override int GetHashCode()
		{
			return this.dependencyRange.GetHashCode() ^ this.referencedRange.GetHashCode();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002CC3 File Offset: 0x00000EC3
		public bool Equals(DocumentRangePair other)
		{
			return this.dependencyRange.Equals(other.dependencyRange) && this.referencedRange.Equals(other.referencedRange);
		}

		// Token: 0x04000077 RID: 119
		private DocumentRange dependencyRange;

		// Token: 0x04000078 RID: 120
		private DocumentRange referencedRange;
	}
}
