using System;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x0200208C RID: 8332
	public class Resource : IEquatable<Resource>, IResource
	{
		// Token: 0x17003117 RID: 12567
		// (get) Token: 0x0600CBE2 RID: 52194 RVA: 0x0028962A File Offset: 0x0028782A
		public static Resource CurrentWorkbookResource
		{
			get
			{
				return new Resource("CurrentWorkbook", string.Empty, string.Empty);
			}
		}

		// Token: 0x0600CBE3 RID: 52195 RVA: 0x000020FD File Offset: 0x000002FD
		public Resource()
		{
		}

		// Token: 0x0600CBE4 RID: 52196 RVA: 0x00289640 File Offset: 0x00287840
		public Resource(string kind, string path, string nonNormalizedPath)
		{
			this.kind = kind;
			this.path = path;
			this.NonNormalizedPath = nonNormalizedPath;
		}

		// Token: 0x0600CBE5 RID: 52197 RVA: 0x0028965D File Offset: 0x0028785D
		public Resource(IResource resource)
			: this(resource.Kind, resource.Path, resource.NonNormalizedPath)
		{
		}

		// Token: 0x17003118 RID: 12568
		// (get) Token: 0x0600CBE6 RID: 52198 RVA: 0x00289677 File Offset: 0x00287877
		// (set) Token: 0x0600CBE7 RID: 52199 RVA: 0x0028967F File Offset: 0x0028787F
		[XmlAttribute("Kind")]
		public string Kind
		{
			get
			{
				return this.kind;
			}
			set
			{
				this.kind = value;
			}
		}

		// Token: 0x17003119 RID: 12569
		// (get) Token: 0x0600CBE8 RID: 52200 RVA: 0x00289688 File Offset: 0x00287888
		// (set) Token: 0x0600CBE9 RID: 52201 RVA: 0x00289690 File Offset: 0x00287890
		[XmlAttribute("Path")]
		public string Path
		{
			get
			{
				return this.path;
			}
			set
			{
				this.path = value;
			}
		}

		// Token: 0x1700311A RID: 12570
		// (get) Token: 0x0600CBEA RID: 52202 RVA: 0x00289699 File Offset: 0x00287899
		// (set) Token: 0x0600CBEB RID: 52203 RVA: 0x002896B5 File Offset: 0x002878B5
		[XmlAttribute("NonNormalizedPath")]
		public string NonNormalizedPath
		{
			get
			{
				if (this.nonNormalizedPath == null)
				{
					this.nonNormalizedPath = this.path;
				}
				return this.nonNormalizedPath;
			}
			set
			{
				this.nonNormalizedPath = value ?? this.path;
			}
		}

		// Token: 0x0600CBEC RID: 52204 RVA: 0x002896C8 File Offset: 0x002878C8
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Resource);
		}

		// Token: 0x0600CBED RID: 52205 RVA: 0x002896D6 File Offset: 0x002878D6
		public override int GetHashCode()
		{
			return Resource.GetHashCode(this.kind, this.path);
		}

		// Token: 0x0600CBEE RID: 52206 RVA: 0x002896E9 File Offset: 0x002878E9
		public bool Equals(Resource other)
		{
			return other != null && Resource.Equals(this.kind, this.path, other.kind, other.path);
		}

		// Token: 0x0600CBEF RID: 52207 RVA: 0x00289712 File Offset: 0x00287912
		public static int GetHashCode(string kind, string path)
		{
			if (path == null)
			{
				return Resource.GetResourceKindHashCode(kind);
			}
			return Resource.GetResourceKindHashCode(kind) ^ Resource.GetResourcePathHashCode(path);
		}

		// Token: 0x0600CBF0 RID: 52208 RVA: 0x0028972B File Offset: 0x0028792B
		public static bool Equals(string kind1, string path1, string kind2, string path2)
		{
			return Resource.AreResourceKindsEqual(kind1, kind2) && Resource.AreResourcePathsEqual(path1, path2);
		}

		// Token: 0x0600CBF1 RID: 52209 RVA: 0x0028973F File Offset: 0x0028793F
		public static bool AreResourceKindsEqual(string resourceKind1, string resourceKind2)
		{
			return resourceKind1 == resourceKind2;
		}

		// Token: 0x0600CBF2 RID: 52210 RVA: 0x0028973F File Offset: 0x0028793F
		public static bool AreResourcePathsEqual(string resourcePath1, string resourcePath2)
		{
			return resourcePath1 == resourcePath2;
		}

		// Token: 0x0600CBF3 RID: 52211 RVA: 0x00289748 File Offset: 0x00287948
		public static int GetResourceKindHashCode(string kind)
		{
			return kind.GetHashCode();
		}

		// Token: 0x0600CBF4 RID: 52212 RVA: 0x00289748 File Offset: 0x00287948
		public static int GetResourcePathHashCode(string path)
		{
			return path.GetHashCode();
		}

		// Token: 0x0600CBF5 RID: 52213 RVA: 0x00289750 File Offset: 0x00287950
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.kind);
			stringBuilder.Append('/');
			stringBuilder.Append(this.path);
			return stringBuilder.ToString();
		}

		// Token: 0x04006768 RID: 26472
		private string kind;

		// Token: 0x04006769 RID: 26473
		private string path;

		// Token: 0x0400676A RID: 26474
		private string nonNormalizedPath;
	}
}
