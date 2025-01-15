using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x02000150 RID: 336
	[DebuggerDisplay("TmdlTranslationElement - type={ObjectType}, name=\"{Name}\"")]
	internal sealed class TmdlTranslationElement
	{
		// Token: 0x0600157A RID: 5498 RVA: 0x0009038E File Offset: 0x0008E58E
		public TmdlTranslationElement(ObjectType objectType)
		{
			this.ObjectType = objectType;
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x0009039D File Offset: 0x0008E59D
		// (set) Token: 0x0600157C RID: 5500 RVA: 0x000903A5 File Offset: 0x0008E5A5
		public TmdlSourceLocation SourceLocation { get; internal set; }

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x000903AE File Offset: 0x0008E5AE
		public ObjectType ObjectType { get; }

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x000903B6 File Offset: 0x0008E5B6
		// (set) Token: 0x0600157F RID: 5503 RVA: 0x000903BE File Offset: 0x0008E5BE
		public ObjectName Name { get; set; }

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x000903C7 File Offset: 0x0008E5C7
		public ICollection<TmdlProperty> Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new List<TmdlProperty>();
				}
				return this.properties;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001581 RID: 5505 RVA: 0x000903E2 File Offset: 0x0008E5E2
		public ICollection<TmdlTranslationElement> Children
		{
			get
			{
				if (this.children == null)
				{
					this.children = new List<TmdlTranslationElement>();
				}
				return this.children;
			}
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x00090400 File Offset: 0x0008E600
		public void WriteTo(ITmdlWriter writer)
		{
			string text = writer.FormatKeyword(this.ObjectType.ToString("G"));
			string text2 = (this.Name.IsEmpty ? string.Empty : string.Format(CultureInfo.InvariantCulture, " {0}", this.Name));
			writer.WriteLine("{0}{1}", new object[] { text, text2 });
			if ((this.properties != null && this.properties.Count > 0) || (this.children != null && this.children.Count > 0))
			{
				using (writer.Indent(1))
				{
					if (this.properties != null && this.properties.Count > 0)
					{
						foreach (TmdlProperty tmdlProperty in this.properties.Where((TmdlProperty p) => !p.DoNotSerialize))
						{
							tmdlProperty.WriteTo(writer);
						}
					}
					if (this.children != null && this.children.Count > 0)
					{
						foreach (TmdlTranslationElement tmdlTranslationElement in this.children)
						{
							tmdlTranslationElement.WriteTo(writer);
						}
					}
				}
			}
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0009059C File Offset: 0x0008E79C
		internal void AddContentOf(TmdlTranslationElement other)
		{
			Utils.Verify(other != null);
			Utils.Verify(this.ObjectType == other.ObjectType);
			Utils.Verify((this.Name.IsEmpty && other.Name.IsEmpty) || (!this.Name.IsEmpty && !other.Name.IsEmpty && this.Name == other.Name));
			if (other.properties != null && other.properties.Count > 0)
			{
				if (this.properties == null)
				{
					this.properties = new List<TmdlProperty>();
				}
				using (List<TmdlProperty>.Enumerator enumerator = other.properties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TmdlProperty property = enumerator.Current;
						if (this.properties.Any((TmdlProperty p) => string.Compare(p.Name, property.Name, StringComparison.InvariantCultureIgnoreCase) == 0))
						{
							throw TmdlSerializationException.CreateAmbiguousSourceException(TomSR.TmdlAmbiguousSourceError_DuplicateProperty(property.Name), this, other);
						}
						this.properties.Add(property);
					}
				}
			}
			if (other.children != null && other.children.Count > 0)
			{
				if (this.children == null)
				{
					this.children = new List<TmdlTranslationElement>();
				}
				this.children.AddRange(other.children);
			}
		}

		// Token: 0x040003C5 RID: 965
		private List<TmdlProperty> properties;

		// Token: 0x040003C6 RID: 966
		private List<TmdlTranslationElement> children;
	}
}
