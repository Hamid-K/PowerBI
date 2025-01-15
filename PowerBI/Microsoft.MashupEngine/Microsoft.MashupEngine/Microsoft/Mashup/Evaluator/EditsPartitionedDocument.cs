using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001C96 RID: 7318
	internal sealed class EditsPartitionedDocument : IPartitionedDocument
	{
		// Token: 0x0600B5FF RID: 46591 RVA: 0x0024F3B6 File Offset: 0x0024D5B6
		public EditsPartitionedDocument(IEngine engine, IPartitionedDocument document, IEnumerable<PackageEdit> edits)
		{
			this.engine = engine;
			this.document = document;
			this.sectionEdits = edits.GetSectionEdits();
		}

		// Token: 0x17002D70 RID: 11632
		// (get) Token: 0x0600B600 RID: 46592 RVA: 0x0024F3D8 File Offset: 0x0024D5D8
		private IPackage EditedPackage
		{
			get
			{
				if (this.editedPackage == null)
				{
					Dictionary<string, IPackageSection> dictionary = new Dictionary<string, IPackageSection>();
					foreach (string text in this.document.Package.SectionNames)
					{
						IPackageSection packageSection = this.document.Package.GetSection(text);
						IList<PackageEdit> list;
						if (this.sectionEdits.TryGetValue(text, out list))
						{
							packageSection = new TranslateSourceLocationPackageSection(this.engine, packageSection, new EditsPackageSection(packageSection, list), list);
						}
						dictionary.Add(text, packageSection);
					}
					this.editedPackage = new Package(dictionary);
				}
				return this.editedPackage;
			}
		}

		// Token: 0x17002D71 RID: 11633
		// (get) Token: 0x0600B601 RID: 46593 RVA: 0x0024F48C File Offset: 0x0024D68C
		private IPartitionedDocument EditedDocument
		{
			get
			{
				if (this.editedDocument == null)
				{
					this.editedDocument = this.EditedPackage.PartitionedDocument(this.PartitioningScheme, this.engine);
				}
				return this.editedDocument;
			}
		}

		// Token: 0x17002D72 RID: 11634
		// (get) Token: 0x0600B602 RID: 46594 RVA: 0x0024F4B9 File Offset: 0x0024D6B9
		public IPackage Package
		{
			get
			{
				return this.EditedPackage;
			}
		}

		// Token: 0x17002D73 RID: 11635
		// (get) Token: 0x0600B603 RID: 46595 RVA: 0x0024F4C1 File Offset: 0x0024D6C1
		public PartitioningScheme PartitioningScheme
		{
			get
			{
				return this.document.PartitioningScheme;
			}
		}

		// Token: 0x17002D74 RID: 11636
		// (get) Token: 0x0600B604 RID: 46596 RVA: 0x0024F4CE File Offset: 0x0024D6CE
		public IEnumerable<IPartitionKey> PartitionKeys
		{
			get
			{
				return this.document.PartitionKeys;
			}
		}

		// Token: 0x0600B605 RID: 46597 RVA: 0x0024F4DB File Offset: 0x0024D6DB
		public IEnumerable<IPartitionKey> GetPartitionInputs(IPartitionKey partitionKey)
		{
			return this.document.GetPartitionInputs(partitionKey);
		}

		// Token: 0x0600B606 RID: 46598 RVA: 0x0024F4E9 File Offset: 0x0024D6E9
		public bool IsPartitionInError(IPartitionKey partitionKey)
		{
			return this.document.IsPartitionInError(partitionKey);
		}

		// Token: 0x0600B607 RID: 46599 RVA: 0x0024F4F8 File Offset: 0x0024D6F8
		public SegmentedString GetPartition(IPartitionKey partitionKey)
		{
			int num;
			int num2;
			string partitionSectionOffsetAndLength = this.GetPartitionSectionOffsetAndLength(partitionKey, out num, out num2);
			return this.EditedPackage.GetSection(partitionSectionOffsetAndLength).Text.Substring(num, num2);
		}

		// Token: 0x0600B608 RID: 46600 RVA: 0x0024F529 File Offset: 0x0024D729
		public string GetPartitionSection(IPartitionKey partitionKey)
		{
			return this.document.GetPartitionSection(partitionKey);
		}

		// Token: 0x0600B609 RID: 46601 RVA: 0x0024F538 File Offset: 0x0024D738
		public string GetPartitionSectionOffsetAndLength(IPartitionKey partitionKey, out int offset, out int length)
		{
			int num;
			int num2;
			string partitionSectionOffsetAndLength = this.document.GetPartitionSectionOffsetAndLength(partitionKey, out num, out num2);
			offset = num;
			length = num2;
			foreach (PackageEdit packageEdit in this.sectionEdits[partitionSectionOffsetAndLength].OrderBy((PackageEdit e) => e.Offset))
			{
				if (packageEdit.Offset + packageEdit.Length < num)
				{
					offset += packageEdit.Text.Length - packageEdit.Length;
				}
				else if (packageEdit.Offset >= num && packageEdit.Offset + packageEdit.Length <= num + num2)
				{
					length += packageEdit.Text.Length - packageEdit.Length;
				}
			}
			return partitionSectionOffsetAndLength;
		}

		// Token: 0x0600B60A RID: 46602 RVA: 0x0024F630 File Offset: 0x0024D830
		public bool TryGetOffsetAndLength(string sectionName, TextRange range, out int offset, out int length)
		{
			return this.EditedDocument.TryGetOffsetAndLength(sectionName, range, out offset, out length);
		}

		// Token: 0x0600B60B RID: 46603 RVA: 0x0024F642 File Offset: 0x0024D842
		public IPartitionKey GetPartitionKeyAndOffset(string sectionName, int offset, int length, out int partitionOffset)
		{
			return this.EditedDocument.GetPartitionKeyAndOffset(sectionName, offset, length, out partitionOffset);
		}

		// Token: 0x0600B60C RID: 46604 RVA: 0x0024F654 File Offset: 0x0024D854
		public IEnumerable<PackageEdit> ReplacePartition(IPartitionKey partitionKey, SegmentedString expression)
		{
			int num;
			int num2;
			string partitionSectionOffsetAndLength = this.GetPartitionSectionOffsetAndLength(partitionKey, out num, out num2);
			return new PackageEdit[]
			{
				new PackageEdit(partitionSectionOffsetAndLength, num, num2, expression)
			};
		}

		// Token: 0x0600B60D RID: 46605 RVA: 0x0024F683 File Offset: 0x0024D883
		public IEnumerable<PackageEdit> ReferencePartition(IPartitionKey partitionKey, out string referencingExpression)
		{
			return this.EditedDocument.ReferencePartition(partitionKey, out referencingExpression);
		}

		// Token: 0x0600B60E RID: 46606 RVA: 0x0024F692 File Offset: 0x0024D892
		public IPartitionedDocument ApplyEdits(IEnumerable<PackageEdit> edits)
		{
			return this.EditedPackage.ApplyEdits(edits).PartitionedDocument(this.PartitioningScheme, this.engine);
		}

		// Token: 0x04005CFA RID: 23802
		private readonly IEngine engine;

		// Token: 0x04005CFB RID: 23803
		private readonly IPartitionedDocument document;

		// Token: 0x04005CFC RID: 23804
		private readonly IDictionary<string, IList<PackageEdit>> sectionEdits;

		// Token: 0x04005CFD RID: 23805
		private IPackage editedPackage;

		// Token: 0x04005CFE RID: 23806
		private IPartitionedDocument editedDocument;

		// Token: 0x02001C97 RID: 7319
		private struct SectionAndOffset
		{
			// Token: 0x0600B60F RID: 46607 RVA: 0x0024F6B1 File Offset: 0x0024D8B1
			public SectionAndOffset(string section, int offset)
			{
				this.section = section;
				this.offset = offset;
			}

			// Token: 0x04005CFF RID: 23807
			public readonly string section;

			// Token: 0x04005D00 RID: 23808
			public readonly int offset;

			// Token: 0x02001C98 RID: 7320
			public sealed class EqualityComparer : IEqualityComparer<EditsPartitionedDocument.SectionAndOffset>
			{
				// Token: 0x0600B610 RID: 46608 RVA: 0x0024F6C1 File Offset: 0x0024D8C1
				public int GetHashCode(EditsPartitionedDocument.SectionAndOffset sectionAndOffset)
				{
					return sectionAndOffset.offset;
				}

				// Token: 0x0600B611 RID: 46609 RVA: 0x0024F6C9 File Offset: 0x0024D8C9
				public bool Equals(EditsPartitionedDocument.SectionAndOffset x, EditsPartitionedDocument.SectionAndOffset y)
				{
					return x.section == y.section && x.offset == y.offset;
				}

				// Token: 0x04005D01 RID: 23809
				public static readonly IEqualityComparer<EditsPartitionedDocument.SectionAndOffset> Instance = new EditsPartitionedDocument.SectionAndOffset.EqualityComparer();
			}
		}
	}
}
