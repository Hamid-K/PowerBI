using System;
using System.Data.Entity.Utilities;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200023B RID: 571
	public class DefaultDbModelStore : DbModelStore
	{
		// Token: 0x06001E27 RID: 7719 RVA: 0x0005432E File Offset: 0x0005252E
		public DefaultDbModelStore(string directory)
		{
			Check.NotEmpty(directory, "directory");
			this._directory = directory;
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001E28 RID: 7720 RVA: 0x00054349 File Offset: 0x00052549
		public string Directory
		{
			get
			{
				return this._directory;
			}
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x00054354 File Offset: 0x00052554
		public override DbCompiledModel TryLoad(Type contextType)
		{
			return this.LoadXml<DbCompiledModel>(contextType, delegate(XmlReader reader)
			{
				string defaultSchema = this.GetDefaultSchema(contextType);
				return EdmxReader.Read(reader, defaultSchema);
			});
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x0005438D File Offset: 0x0005258D
		public override XDocument TryGetEdmx(Type contextType)
		{
			return this.LoadXml<XDocument>(contextType, new Func<XmlReader, XDocument>(XDocument.Load));
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x000543A4 File Offset: 0x000525A4
		internal T LoadXml<T>(Type contextType, Func<XmlReader, T> xmlReaderDelegate)
		{
			string filePath = this.GetFilePath(contextType);
			T t;
			if (!File.Exists(filePath))
			{
				t = default(T);
				return t;
			}
			if (!this.FileIsValid(contextType, filePath))
			{
				File.Delete(filePath);
				t = default(T);
				return t;
			}
			using (XmlReader xmlReader = XmlReader.Create(filePath))
			{
				t = xmlReaderDelegate(xmlReader);
			}
			return t;
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x00054414 File Offset: 0x00052614
		public override void Save(Type contextType, DbModel model)
		{
			using (XmlWriter xmlWriter = XmlWriter.Create(this.GetFilePath(contextType), new XmlWriterSettings
			{
				Indent = true
			}))
			{
				EdmxWriter.WriteEdmx(model, xmlWriter);
			}
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x00054460 File Offset: 0x00052660
		protected virtual string GetFilePath(Type contextType)
		{
			string text = contextType.FullName + ".edmx";
			return Path.Combine(this._directory, text);
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x0005448C File Offset: 0x0005268C
		protected virtual bool FileIsValid(Type contextType, string filePath)
		{
			DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(contextType.Assembly.Location);
			return File.GetLastWriteTimeUtc(filePath) >= lastWriteTimeUtc;
		}

		// Token: 0x04000B2C RID: 2860
		private const string FileExtension = ".edmx";

		// Token: 0x04000B2D RID: 2861
		private readonly string _directory;
	}
}
