using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Diagnostics
{
	// Token: 0x020008BD RID: 2237
	public abstract class Location
	{
		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06003024 RID: 12324
		public abstract string FileName { get; }

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06003025 RID: 12325
		public abstract string Position { get; }

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06003026 RID: 12326 RVA: 0x0008E23E File Offset: 0x0008C43E
		public string Message
		{
			get
			{
				return string.Join(":", new string[] { this.FileName, this.Position }).Trim(new char[] { ':' });
			}
		}

		// Token: 0x0400183C RID: 6204
		public static Location.UnknownLocation Unknown = new Location.UnknownLocation();

		// Token: 0x020008BE RID: 2238
		public sealed class Source : Location
		{
			// Token: 0x06003029 RID: 12329 RVA: 0x0008E27E File Offset: 0x0008C47E
			public Source(string fileName, int? line = null, int? column = null)
			{
				this._fileName = fileName;
				this._line = line;
				this._column = column;
			}

			// Token: 0x1700086E RID: 2158
			// (get) Token: 0x0600302A RID: 12330 RVA: 0x0008E29B File Offset: 0x0008C49B
			public override string FileName
			{
				get
				{
					return Path.GetFileName(this._fileName);
				}
			}

			// Token: 0x1700086F RID: 2159
			// (get) Token: 0x0600302B RID: 12331 RVA: 0x0008E2A8 File Offset: 0x0008C4A8
			public override string Position
			{
				get
				{
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}:{1}", new object[] { this._line, this._column })).Trim(new char[] { ':' });
				}
			}

			// Token: 0x0400183D RID: 6205
			private readonly string _fileName;

			// Token: 0x0400183E RID: 6206
			private readonly int? _line;

			// Token: 0x0400183F RID: 6207
			private readonly int? _column;
		}

		// Token: 0x020008BF RID: 2239
		public sealed class Assembly : Location
		{
			// Token: 0x0600302C RID: 12332 RVA: 0x0008E2F6 File Offset: 0x0008C4F6
			public Assembly(string fileName, string namespaceName = null, string className = null, string memberName = null)
			{
				this._fileName = fileName;
				this._namespaceName = namespaceName;
				this._className = className;
				this._memberName = memberName;
			}

			// Token: 0x0600302D RID: 12333 RVA: 0x0008E31B File Offset: 0x0008C51B
			public Assembly(Type type)
				: this(type.GetTypeInfo().Assembly.Location, type.Namespace, type.Name, null)
			{
			}

			// Token: 0x0600302E RID: 12334 RVA: 0x0008E340 File Offset: 0x0008C540
			public Assembly(MemberInfo member)
				: this(member.DeclaringType.GetTypeInfo().Assembly.Location, member.DeclaringType.Namespace, member.DeclaringType.Name, member.Name)
			{
			}

			// Token: 0x17000870 RID: 2160
			// (get) Token: 0x0600302F RID: 12335 RVA: 0x0008E379 File Offset: 0x0008C579
			public override string FileName
			{
				get
				{
					return Path.GetFileName(this._fileName);
				}
			}

			// Token: 0x17000871 RID: 2161
			// (get) Token: 0x06003030 RID: 12336 RVA: 0x0008E386 File Offset: 0x0008C586
			public override string Position
			{
				get
				{
					return string.Join(".", new string[] { this._namespaceName, this._className, this._memberName }).Trim(new char[] { '.' });
				}
			}

			// Token: 0x04001840 RID: 6208
			private readonly string _fileName;

			// Token: 0x04001841 RID: 6209
			private readonly string _namespaceName;

			// Token: 0x04001842 RID: 6210
			private readonly string _className;

			// Token: 0x04001843 RID: 6211
			private readonly string _memberName;
		}

		// Token: 0x020008C0 RID: 2240
		public sealed class UnknownLocation : Location
		{
			// Token: 0x17000872 RID: 2162
			// (get) Token: 0x06003031 RID: 12337 RVA: 0x0008E3C3 File Offset: 0x0008C5C3
			public override string FileName
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x17000873 RID: 2163
			// (get) Token: 0x06003032 RID: 12338 RVA: 0x0008E3C3 File Offset: 0x0008C5C3
			public override string Position
			{
				get
				{
					return string.Empty;
				}
			}
		}
	}
}
