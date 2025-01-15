using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Resources;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002AA RID: 682
	internal class ResourceLoader
	{
		// Token: 0x060018C8 RID: 6344 RVA: 0x000645BC File Offset: 0x000627BC
		public ResourceLoader(string resourceNamespace, Assembly assembly)
		{
			this.m_resourceNamespace = resourceNamespace + ".";
			this.m_assembly = assembly;
			this.m_resourceManager = new ResourceManager(resourceNamespace, assembly);
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x00064628 File Offset: 0x00062828
		public virtual bool LoadImage(string name, CreateAndRegisterStream createAndRegisterStreamCallback)
		{
			string text = (string)this.m_imageHash[name];
			if (text != null)
			{
				ProcessingContext.LoadImageResource(text, this.m_resourceManager, createAndRegisterStreamCallback);
				return true;
			}
			return false;
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x0006465C File Offset: 0x0006285C
		public virtual bool LoadFile(string name, CreateAndRegisterStream createAndRegisterStreamCallback)
		{
			string text = (string)this.m_fileHash[name];
			if (text != null)
			{
				Stream stream = createAndRegisterStreamCallback(name, string.Empty, null, null, false, StreamOper.CreateAndRegister);
				StreamSupport.CopyStreamUsingBuffer(this.m_assembly.GetManifestResourceStream(this.m_resourceNamespace + text), stream, 16384);
				return true;
			}
			return false;
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x000646B5 File Offset: 0x000628B5
		public static string MakeNativeUrlResource(string resourceName)
		{
			return ResourceLoader.ResourcePrefix + resourceName;
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x000646C2 File Offset: 0x000628C2
		private static string UrlPrefix
		{
			get
			{
				return ResourceLoader.m_assemblyVersion;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x000646C9 File Offset: 0x000628C9
		private static string ResourcePrefix
		{
			get
			{
				return RSConfiguration.NativeServerProductVersion;
			}
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x000646D0 File Offset: 0x000628D0
		public static byte[] GetStyleSheet(string requestedStyleSheet, string styleResourceParentFolder, RSTrace tracer)
		{
			byte[] array = ResourceLoader.GetStyleResource(requestedStyleSheet, styleResourceParentFolder, "User specified", tracer);
			if (array == null)
			{
				string defaultViewerStyleSheet = Globals.Configuration.DefaultViewerStyleSheet;
				if (defaultViewerStyleSheet != null && defaultViewerStyleSheet.Length > 0)
				{
					array = ResourceLoader.GetStyleResource(defaultViewerStyleSheet, styleResourceParentFolder, "Config File", tracer);
				}
			}
			return array;
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x00064714 File Offset: 0x00062914
		public static byte[] GetStyleSheetImage(string imageName, string imageParentFolder, RSTrace tracer)
		{
			return ResourceLoader.GetStyleResource(imageName, imageParentFolder, "User specified", tracer);
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x00064724 File Offset: 0x00062924
		private static byte[] GetStyleResource(string styleResourceName, string styleResourceFolder, string sourceTraceName, RSTrace tracer)
		{
			if (!string.IsNullOrEmpty(styleResourceName))
			{
				if (styleResourceName.IndexOfAny(Path.GetInvalidFileNameChars()) == -1 && styleResourceName.Trim().Length > 0)
				{
					string text = Path.Combine(styleResourceFolder, styleResourceName);
					if (string.IsNullOrEmpty(Path.GetExtension(text)))
					{
						text += ".css";
					}
					try
					{
						if (File.Exists(text))
						{
							return File.ReadAllBytes(text);
						}
						if (tracer.TraceInfo)
						{
							tracer.Trace(TraceLevel.Info, "{0} style sheet or image does not exist: {1}", new object[] { sourceTraceName, text });
						}
						goto IL_00D4;
					}
					catch (Exception ex)
					{
						if (tracer.TraceWarning)
						{
							tracer.Trace(TraceLevel.Warning, "Unable to read {0} style sheet or image: {1}.  {2}", new object[]
							{
								sourceTraceName,
								text,
								ex.ToString()
							});
						}
						goto IL_00D4;
					}
				}
				if (tracer.TraceInfo)
				{
					tracer.Trace(TraceLevel.Info, "{0} an invalid style sheet name or image: {1}", new object[] { sourceTraceName, styleResourceName });
				}
			}
			IL_00D4:
			return null;
		}

		// Token: 0x040008EC RID: 2284
		private static string m_assemblyVersion = Globals.Configuration.ServerProductVersion;

		// Token: 0x040008ED RID: 2285
		protected Hashtable m_imageHash = CollectionsUtil.CreateCaseInsensitiveHashtable();

		// Token: 0x040008EE RID: 2286
		protected Hashtable m_fileHash = CollectionsUtil.CreateCaseInsensitiveHashtable();

		// Token: 0x040008EF RID: 2287
		private ResourceManager m_resourceManager;

		// Token: 0x040008F0 RID: 2288
		protected string m_resourceNamespace;

		// Token: 0x040008F1 RID: 2289
		protected Assembly m_assembly;
	}
}
