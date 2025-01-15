using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000077 RID: 119
	[Guid("09AEB310-E5ED-4ba2-A58B-440FA42CB09C")]
	public sealed class ClrAssemblyFile : ICloneable
	{
		// Token: 0x0600065B RID: 1627 RVA: 0x000233B6 File Offset: 0x000215B6
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x000233BE File Offset: 0x000215BE
		public ClrAssemblyFile()
		{
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x000233D1 File Offset: 0x000215D1
		public ClrAssemblyFile(string name)
		{
			this.Name = name;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x000233EB File Offset: 0x000215EB
		public ClrAssemblyFile(string name, ClrAssemblyFileType type)
		{
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x0002340C File Offset: 0x0002160C
		// (set) Token: 0x06000660 RID: 1632 RVA: 0x00023414 File Offset: 0x00021614
		[XmlElement(IsNullable = false)]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x0002341D File Offset: 0x0002161D
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x00023425 File Offset: 0x00021625
		public ClrAssemblyFileType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x0002342E File Offset: 0x0002162E
		[XmlArray]
		[XmlArrayItem("Block")]
		[Browsable(false)]
		public StringCollection Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00023436 File Offset: 0x00021636
		public ClrAssemblyFile CopyTo(ClrAssemblyFile obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			obj.Name = this.Name;
			obj.Type = this.Type;
			Utils.Copy(this.Data, obj.Data);
			return obj;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00023471 File Offset: 0x00021671
		public ClrAssemblyFile Clone()
		{
			return this.CopyTo(new ClrAssemblyFile());
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00023480 File Offset: 0x00021680
		public void LoadData(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (!File.Exists(path))
			{
				throw new FileNotFoundException(SR.ClrAssemblyFile_FileNotFound(path));
			}
			FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
			byte[] array = new byte[1024];
			try
			{
				this.data.Clear();
				for (int i = fileStream.Read(array, 0, 1024); i > 0; i = fileStream.Read(array, 0, 1024))
				{
					this.data.Add(Convert.ToBase64String(array, 0, i));
				}
			}
			finally
			{
				fileStream.Close();
			}
		}

		// Token: 0x04000420 RID: 1056
		private const int DataLoadingBufferSize = 1024;

		// Token: 0x04000421 RID: 1057
		private string name;

		// Token: 0x04000422 RID: 1058
		private ClrAssemblyFileType type;

		// Token: 0x04000423 RID: 1059
		private StringCollection data = new StringCollection();
	}
}
