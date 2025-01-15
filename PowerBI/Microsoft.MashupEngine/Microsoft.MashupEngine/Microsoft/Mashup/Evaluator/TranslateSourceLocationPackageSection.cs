using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D76 RID: 7542
	internal sealed class TranslateSourceLocationPackageSection : IPackageSection, IDocumentHost, ITranslateSourceLocation, ICacheableDocumentHost
	{
		// Token: 0x0600BB59 RID: 47961 RVA: 0x0025F05C File Offset: 0x0025D25C
		public TranslateSourceLocationPackageSection(IEngine engine, IPackageSection fromSection, IPackageSection toSection, IEnumerable<PackageEdit> sectionEdits)
		{
			this.engine = engine;
			this.fromSection = fromSection;
			this.toSection = toSection;
			this.sectionEdits = sectionEdits.OrderBy((PackageEdit e) => e.Offset).ToArray<PackageEdit>();
		}

		// Token: 0x17002E3F RID: 11839
		// (get) Token: 0x0600BB5A RID: 47962 RVA: 0x0025F0B5 File Offset: 0x0025D2B5
		public string UniqueID
		{
			get
			{
				return this.toSection.UniqueID;
			}
		}

		// Token: 0x17002E40 RID: 11840
		// (get) Token: 0x0600BB5B RID: 47963 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public object CacheIdentity
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17002E41 RID: 11841
		// (get) Token: 0x0600BB5C RID: 47964 RVA: 0x0025F0C2 File Offset: 0x0025D2C2
		public IPackageSectionConfig Config
		{
			get
			{
				return this.toSection.Config;
			}
		}

		// Token: 0x17002E42 RID: 11842
		// (get) Token: 0x0600BB5D RID: 47965 RVA: 0x0025F0CF File Offset: 0x0025D2CF
		public SegmentedString Text
		{
			get
			{
				return this.toSection.Text;
			}
		}

		// Token: 0x0600BB5E RID: 47966 RVA: 0x0025F0DC File Offset: 0x0025D2DC
		public SourceLocation TranslateSourceLocation(SourceLocation location)
		{
			location = new SourceLocation(this.fromSection, this.MapRange(location.Range));
			ITranslateSourceLocation translateSourceLocation = location.Document as ITranslateSourceLocation;
			if (translateSourceLocation != null)
			{
				location = translateSourceLocation.TranslateSourceLocation(location);
			}
			return location;
		}

		// Token: 0x0600BB5F RID: 47967 RVA: 0x0025F11C File Offset: 0x0025D31C
		public bool Equals(TranslateSourceLocationPackageSection other)
		{
			if (other != null)
			{
				ICacheableDocumentHost cacheableDocumentHost = this.fromSection as ICacheableDocumentHost;
				ICacheableDocumentHost cacheableDocumentHost2 = this.toSection as ICacheableDocumentHost;
				ICacheableDocumentHost cacheableDocumentHost3 = other.fromSection as ICacheableDocumentHost;
				ICacheableDocumentHost cacheableDocumentHost4 = other.toSection as ICacheableDocumentHost;
				if (cacheableDocumentHost != null && cacheableDocumentHost2 != null && cacheableDocumentHost3 != null && cacheableDocumentHost4 != null)
				{
					return cacheableDocumentHost.CacheIdentity.Equals(cacheableDocumentHost3.CacheIdentity) && cacheableDocumentHost2.CacheIdentity.Equals(cacheableDocumentHost4.CacheIdentity);
				}
			}
			return this == other;
		}

		// Token: 0x0600BB60 RID: 47968 RVA: 0x0025F193 File Offset: 0x0025D393
		public override bool Equals(object other)
		{
			return this.Equals(other as TranslateSourceLocationPackageSection);
		}

		// Token: 0x0600BB61 RID: 47969 RVA: 0x0025F1A4 File Offset: 0x0025D3A4
		public override int GetHashCode()
		{
			ICacheableDocumentHost cacheableDocumentHost = this.fromSection as ICacheableDocumentHost;
			ICacheableDocumentHost cacheableDocumentHost2 = this.toSection as ICacheableDocumentHost;
			if (cacheableDocumentHost != null && cacheableDocumentHost2 != null)
			{
				return cacheableDocumentHost.CacheIdentity.GetHashCode() + 29 * cacheableDocumentHost2.CacheIdentity.GetHashCode();
			}
			return base.GetHashCode();
		}

		// Token: 0x0600BB62 RID: 47970 RVA: 0x0025F1F0 File Offset: 0x0025D3F0
		private TextRange MapRange(TextRange toRange)
		{
			int num2;
			int num = this.GetOffsetAndLength(this.toSection.Text, toRange, out num2);
			num = TranslateSourceLocationPackageSection.MapOffsetAndLength(this.sectionEdits, num, num2, out num2);
			return this.GetTextRange(this.fromSection.Text, num, num2);
		}

		// Token: 0x0600BB63 RID: 47971 RVA: 0x0025F238 File Offset: 0x0025D438
		private int GetOffsetAndLength(SegmentedString text, TextRange range, out int length)
		{
			ITokens tokens = this.engine.Tokenize(text);
			int offset = tokens.GetOffset(range.Start);
			int offset2 = tokens.GetOffset(range.End);
			length = offset2 - offset;
			return offset;
		}

		// Token: 0x0600BB64 RID: 47972 RVA: 0x0025F274 File Offset: 0x0025D474
		private TextRange GetTextRange(SegmentedString text, int offset, int length)
		{
			ITokens tokens = this.engine.Tokenize(text);
			TextPosition position = tokens.GetPosition(offset);
			TextPosition position2 = tokens.GetPosition(offset + length);
			return new TextRange(position, position2);
		}

		// Token: 0x0600BB65 RID: 47973 RVA: 0x0025F2A8 File Offset: 0x0025D4A8
		private static int MapOffsetAndLength(IEnumerable<PackageEdit> orderedEdits, int toOffset, int toLength, out int fromLength)
		{
			List<TranslateSourceLocationPackageSection.ReverseEdit> list = new List<TranslateSourceLocationPackageSection.ReverseEdit>();
			int num = 0;
			foreach (PackageEdit packageEdit in orderedEdits)
			{
				list.Add(new TranslateSourceLocationPackageSection.ReverseEdit
				{
					toOffset = packageEdit.Offset + num,
					fromLength = packageEdit.Length,
					toLength = packageEdit.Text.Length
				});
				num += packageEdit.Text.Length - packageEdit.Length;
				int offset = packageEdit.Offset;
				int length = packageEdit.Length;
			}
			fromLength = toLength;
			int num2 = toOffset;
			foreach (TranslateSourceLocationPackageSection.ReverseEdit reverseEdit in list.OrderByDescending((TranslateSourceLocationPackageSection.ReverseEdit m) => m.toOffset))
			{
				if (reverseEdit.toOffset + reverseEdit.toLength <= toOffset)
				{
					num2 -= reverseEdit.toLength - reverseEdit.fromLength;
				}
				else if (reverseEdit.toOffset >= toOffset && reverseEdit.toOffset + reverseEdit.toLength <= toOffset + toLength)
				{
					fromLength += reverseEdit.fromLength - reverseEdit.toLength;
				}
			}
			return num2;
		}

		// Token: 0x04005F64 RID: 24420
		private readonly IEngine engine;

		// Token: 0x04005F65 RID: 24421
		private readonly IPackageSection fromSection;

		// Token: 0x04005F66 RID: 24422
		private readonly IPackageSection toSection;

		// Token: 0x04005F67 RID: 24423
		private readonly PackageEdit[] sectionEdits;

		// Token: 0x02001D77 RID: 7543
		private struct ReverseEdit
		{
			// Token: 0x04005F68 RID: 24424
			public int toOffset;

			// Token: 0x04005F69 RID: 24425
			public int fromLength;

			// Token: 0x04005F6A RID: 24426
			public int toLength;
		}
	}
}
