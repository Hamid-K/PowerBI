using System;
using System.IO;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x02000018 RID: 24
	public class RSPath : IEquatable<RSPath>
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00003AE0 File Offset: 0x00001CE0
		public RSPath(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("RSPath cannot be created with a null string");
			}
			this._path = path;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003AFD File Offset: 0x00001CFD
		public override string ToString()
		{
			return this._path;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003B05 File Offset: 0x00001D05
		public RSPath DirectoryName
		{
			get
			{
				return new RSPath(Path.GetDirectoryName(this._path));
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003B17 File Offset: 0x00001D17
		public RSPath FileName
		{
			get
			{
				return new RSPath(Path.GetFileName(this._path));
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003B29 File Offset: 0x00001D29
		public RSPath FileNameWithoutExtension
		{
			get
			{
				return new RSPath(Path.GetFileNameWithoutExtension(this._path));
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003B3B File Offset: 0x00001D3B
		public RSPath FullPath
		{
			get
			{
				return new RSPath(Path.GetFullPath(this._path));
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003B50 File Offset: 0x00001D50
		public RSPath Append(string pathPart)
		{
			string text = this._path;
			if (Path.GetPathRoot(this._path) == this._path)
			{
				text = this._path + "\\";
			}
			return new RSPath(Path.Combine(text, pathPart));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003B99 File Offset: 0x00001D99
		public static RSPath Combine(params string[] parts)
		{
			return new RSPath(Path.Combine(parts));
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003BA6 File Offset: 0x00001DA6
		public RSPath ChangeExtension(string newExtension)
		{
			return new RSPath(Path.ChangeExtension(this._path, newExtension));
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003BB9 File Offset: 0x00001DB9
		public RSPath PathRoot
		{
			get
			{
				return new RSPath(Path.GetPathRoot(this._path));
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003BCC File Offset: 0x00001DCC
		public object IsValid
		{
			get
			{
				try
				{
					Path.GetFullPath(this._path);
				}
				catch (Exception)
				{
					return false;
				}
				return true;
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003C0C File Offset: 0x00001E0C
		public override int GetHashCode()
		{
			return this._path.GetHashCode();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003C1C File Offset: 0x00001E1C
		public override bool Equals(object obj)
		{
			RSPath rspath = obj as RSPath;
			return this == rspath;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003C37 File Offset: 0x00001E37
		public static RSPath operator +(RSPath current, string added)
		{
			return new RSPath(Path.Combine(current._path, added));
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003C4A File Offset: 0x00001E4A
		public static implicit operator string(RSPath current)
		{
			if (!(current != null))
			{
				return null;
			}
			return current._path;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003C5D File Offset: 0x00001E5D
		public static implicit operator RSPath(string current)
		{
			if (current == null)
			{
				return null;
			}
			return new RSPath(current);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003C6A File Offset: 0x00001E6A
		public static bool operator ==(RSPath path1, RSPath path2)
		{
			return path1 == path2 || (path1 != null && path2 != null && string.Compare(path1._path, path2._path, StringComparison.InvariantCultureIgnoreCase) == 0);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003C8F File Offset: 0x00001E8F
		public static bool operator !=(RSPath path1, RSPath path2)
		{
			return !(path1 == path2);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003C9B File Offset: 0x00001E9B
		public bool Equals(RSPath other)
		{
			return this == other;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003CA4 File Offset: 0x00001EA4
		public RSPath TrimTrailingSlash()
		{
			if (!this._path.EndsWith("\\"))
			{
				return this;
			}
			return new RSPath(this._path.TrimEnd(new char[] { '\\' }));
		}

		// Token: 0x04000071 RID: 113
		private readonly string _path;
	}
}
