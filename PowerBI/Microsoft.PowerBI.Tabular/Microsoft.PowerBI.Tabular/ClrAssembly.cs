using System;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000D1 RID: 209
	[Guid("247B8E26-41E3-460f-9996-F174511341D5")]
	public sealed class ClrAssembly : Microsoft.AnalysisServices.Tabular.Assembly, IMajorObject
	{
		// Token: 0x06000D32 RID: 3378 RVA: 0x0006C87F File Offset: 0x0006AA7F
		void IMajorObject.CreateBody()
		{
			base.CreateBody();
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0006C887 File Offset: 0x0006AA87
		private protected override MajorObject.MajorObjectBody CreateBodyImpl()
		{
			return new ClrAssembly.ClrAssemblyBody(this);
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0006C890 File Offset: 0x0006AA90
		protected internal override MajorObject Clone(bool forceBodyLoading)
		{
			MajorObject majorObject = new ClrAssembly();
			this.CopyTo(majorObject, forceBodyLoading);
			return majorObject;
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0006C8AC File Offset: 0x0006AAAC
		protected internal override void CopyTo(MajorObject destination, bool forceBodyLoading)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			ClrAssembly clrAssembly = destination as ClrAssembly;
			if (clrAssembly == null)
			{
				throw new ArgumentException(SR.Copy_InvalidDestination, "destination");
			}
			base.CopyTo(destination, forceBodyLoading);
			if (!base.IsLoaded && !forceBodyLoading)
			{
				return;
			}
			this.Files.CopyTo(clrAssembly.Files);
			clrAssembly.PermissionSet = this.PermissionSet;
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0006C912 File Offset: 0x0006AB12
		public ClrAssembly()
		{
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0006C91A File Offset: 0x0006AB1A
		public ClrAssembly(string name)
			: base(name)
		{
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0006C923 File Offset: 0x0006AB23
		public ClrAssembly(string name, string id)
			: base(name, id)
		{
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x0006C92D File Offset: 0x0006AB2D
		[XmlArray]
		[XmlArrayItem("File", typeof(ClrAssemblyFile))]
		public ClrAssemblyFileCollection Files
		{
			get
			{
				return base.GetBody<ClrAssembly.ClrAssemblyBody>().Files;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x0006C93A File Offset: 0x0006AB3A
		// (set) Token: 0x06000D3B RID: 3387 RVA: 0x0006C947 File Offset: 0x0006AB47
		[LocalizedDescription("PropertyDescription_ClrAssembly_PermissionSet")]
		public PermissionSet PermissionSet
		{
			get
			{
				return base.GetBody<ClrAssembly.ClrAssemblyBody>().PermissionSet;
			}
			set
			{
				base.GetBody<ClrAssembly.ClrAssemblyBody>().PermissionSet = value;
			}
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0006C955 File Offset: 0x0006AB55
		public ClrAssembly CopyTo(ClrAssembly obj)
		{
			this.CopyTo(obj, true);
			return obj;
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0006C960 File Offset: 0x0006AB60
		public override Microsoft.AnalysisServices.Tabular.Assembly Clone()
		{
			return this.CopyTo(new ClrAssembly());
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0006C970 File Offset: 0x0006AB70
		public void LoadFiles(string mainFilePath, bool loadPdbs)
		{
			if (mainFilePath == null)
			{
				throw new ArgumentNullException("mainFilePath");
			}
			if (!File.Exists(mainFilePath))
			{
				throw new FileNotFoundException(SR.ClrAssembly_AssemblyFileNotFound(mainFilePath), mainFilePath);
			}
			AppDomain appDomain = AppDomain.CreateDomain("temp");
			StringCollection stringCollection = new StringCollection();
			try
			{
				((AssemblyReferencesHelper)appDomain.CreateInstanceAndUnwrap(global::System.Reflection.Assembly.GetExecutingAssembly().FullName, typeof(AssemblyReferencesHelper).FullName)).GetAssemblyReferences(mainFilePath, ref stringCollection);
			}
			finally
			{
				AppDomain.Unload(appDomain);
				appDomain = null;
			}
			ClrAssemblyFileCollection files = this.Files;
			files.Clear();
			int i = 0;
			int count = stringCollection.Count;
			while (i < count)
			{
				string text = stringCollection[i];
				ClrAssemblyFile clrAssemblyFile = new ClrAssemblyFile(Path.GetFileName(text));
				clrAssemblyFile.Type = ((i == 0) ? ClrAssemblyFileType.Main : ClrAssemblyFileType.Dependent);
				clrAssemblyFile.LoadData(text);
				files.Add(clrAssemblyFile);
				if (loadPdbs)
				{
					string text2 = Path.GetFileNameWithoutExtension(text) + ".pdb";
					string text3 = Path.Combine(Path.GetDirectoryName(text), text2);
					if (File.Exists(text3))
					{
						clrAssemblyFile = new ClrAssemblyFile(text2, ClrAssemblyFileType.Debug);
						clrAssemblyFile.LoadData(text3);
						files.Add(clrAssemblyFile);
					}
				}
				i++;
			}
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0006CAA4 File Offset: 0x0006ACA4
		internal override bool IsSyntacticallyValidID(string newValue, Type type, out string error)
		{
			return Utils.IsSyntacticallyValidID(newValue, type, out error);
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0006CAAE File Offset: 0x0006ACAE
		internal override bool IsValidName(string newValue, Type type, ModelType modelType, int compatibilityLevel, NamedComponentCollection namedComponentCollection, out string error)
		{
			return Utils.IsValidName(newValue, type, modelType, compatibilityLevel, namedComponentCollection, out error);
		}

		// Token: 0x04000190 RID: 400
		private const string TempAppDomainName = "temp";

		// Token: 0x020002E8 RID: 744
		private sealed class ClrAssemblyBody : Microsoft.AnalysisServices.Tabular.Assembly.AssemblyBody
		{
			// Token: 0x060023AF RID: 9135 RVA: 0x000E2924 File Offset: 0x000E0B24
			public ClrAssemblyBody(ClrAssembly owner)
				: base(owner)
			{
				this.Files = new ClrAssemblyFileCollection();
				this.PermissionSet = PermissionSet.Safe;
			}

			// Token: 0x04000AB7 RID: 2743
			public ClrAssemblyFileCollection Files;

			// Token: 0x04000AB8 RID: 2744
			public PermissionSet PermissionSet;
		}
	}
}
