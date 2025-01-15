using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml;
using JetBrains.Annotations;
using NLog.Common;
using NLog.Internal;
using NLog.Layouts;

namespace NLog.Config
{
	// Token: 0x020001A1 RID: 417
	public class XmlLoggingConfiguration : LoggingConfigurationParser
	{
		// Token: 0x060012CA RID: 4810 RVA: 0x0003306C File Offset: 0x0003126C
		public XmlLoggingConfiguration(string fileName)
			: this(fileName, LogManager.LogFactory)
		{
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x0003307A File Offset: 0x0003127A
		public XmlLoggingConfiguration(string fileName, LogFactory logFactory)
			: this(fileName, false, logFactory)
		{
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x00033085 File Offset: 0x00031285
		public XmlLoggingConfiguration(string fileName, bool ignoreErrors)
			: this(fileName, ignoreErrors, LogManager.LogFactory)
		{
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00033094 File Offset: 0x00031294
		public XmlLoggingConfiguration(string fileName, bool ignoreErrors, LogFactory logFactory)
		{
			this._fileMustAutoReloadLookup = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
			this._currentFilePath = new Stack<string>();
			base..ctor(logFactory);
			using (XmlReader xmlReader = XmlLoggingConfiguration.CreateFileReader(fileName))
			{
				this.Initialize(xmlReader, fileName, ignoreErrors);
			}
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x000330F0 File Offset: 0x000312F0
		private static XmlReader CreateFileReader(string fileName)
		{
			if (!string.IsNullOrEmpty(fileName))
			{
				fileName = fileName.Trim();
				return XmlReader.Create(fileName);
			}
			return null;
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0003310A File Offset: 0x0003130A
		public XmlLoggingConfiguration(XmlReader reader, string fileName)
			: this(reader, fileName, LogManager.LogFactory)
		{
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x00033119 File Offset: 0x00031319
		public XmlLoggingConfiguration(XmlReader reader, string fileName, LogFactory logFactory)
			: this(reader, fileName, false, logFactory)
		{
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x00033125 File Offset: 0x00031325
		public XmlLoggingConfiguration(XmlReader reader, string fileName, bool ignoreErrors)
			: this(reader, fileName, ignoreErrors, LogManager.LogFactory)
		{
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x00033135 File Offset: 0x00031335
		public XmlLoggingConfiguration(XmlReader reader, string fileName, bool ignoreErrors, LogFactory logFactory)
		{
			this._fileMustAutoReloadLookup = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
			this._currentFilePath = new Stack<string>();
			base..ctor(logFactory);
			this.Initialize(reader, fileName, ignoreErrors);
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x00033163 File Offset: 0x00031363
		internal XmlLoggingConfiguration(global::System.Xml.XmlElement element, string fileName)
			: this(element, fileName, false)
		{
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x0003316E File Offset: 0x0003136E
		internal XmlLoggingConfiguration(global::System.Xml.XmlElement element, string fileName, bool ignoreErrors)
			: this(element.OuterXml, fileName, ignoreErrors)
		{
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x00033180 File Offset: 0x00031380
		internal XmlLoggingConfiguration(string xmlContents, string fileName, bool ignoreErrors)
		{
			this._fileMustAutoReloadLookup = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
			this._currentFilePath = new Stack<string>();
			base..ctor(LogManager.LogFactory);
			using (StringReader stringReader = new StringReader(xmlContents))
			{
				using (XmlReader xmlReader = XmlReader.Create(stringReader))
				{
					this.Initialize(xmlReader, fileName, ignoreErrors);
				}
			}
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x000331FC File Offset: 0x000313FC
		public static XmlLoggingConfiguration CreateFromXmlString(string xml)
		{
			return new XmlLoggingConfiguration(xml, null, false);
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x00033206 File Offset: 0x00031406
		public static LoggingConfiguration AppConfig
		{
			get
			{
				return global::System.Configuration.ConfigurationManager.GetSection("nlog") as LoggingConfiguration;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x00033217 File Offset: 0x00031417
		// (set) Token: 0x060012D9 RID: 4825 RVA: 0x0003321F File Offset: 0x0003141F
		public bool? InitializeSucceeded { get; private set; }

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x00033228 File Offset: 0x00031428
		// (set) Token: 0x060012DB RID: 4827 RVA: 0x00033268 File Offset: 0x00031468
		public bool AutoReload
		{
			get
			{
				if (this._fileMustAutoReloadLookup.Count == 0)
				{
					return false;
				}
				return this._fileMustAutoReloadLookup.Values.All((bool mustAutoReload) => mustAutoReload);
			}
			set
			{
				foreach (string text in this._fileMustAutoReloadLookup.Keys.ToList<string>())
				{
					this._fileMustAutoReloadLookup[text] = value;
				}
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x000332CC File Offset: 0x000314CC
		public override IEnumerable<string> FileNamesToWatch
		{
			get
			{
				return from entry in this._fileMustAutoReloadLookup
					where entry.Value
					select entry.Key;
			}
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00033327 File Offset: 0x00031527
		public override LoggingConfiguration Reload()
		{
			if (!string.IsNullOrEmpty(this._originalFileName))
			{
				return new XmlLoggingConfiguration(this._originalFileName, base.LogFactory);
			}
			return base.Reload();
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x0003334E File Offset: 0x0003154E
		public static IEnumerable<string> GetCandidateConfigFilePaths()
		{
			return LogManager.LogFactory.GetCandidateConfigFilePaths();
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x0003335A File Offset: 0x0003155A
		public static void SetCandidateConfigFilePaths(IEnumerable<string> filePaths)
		{
			LogManager.LogFactory.SetCandidateConfigFilePaths(filePaths);
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00033367 File Offset: 0x00031567
		public static void ResetCandidateConfigFilePath()
		{
			LogManager.LogFactory.ResetCandidateConfigFilePath();
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x00033374 File Offset: 0x00031574
		private void Initialize([NotNull] XmlReader reader, [CanBeNull] string fileName, bool ignoreErrors)
		{
			try
			{
				this.InitializeSucceeded = null;
				this._originalFileName = fileName;
				reader.MoveToContent();
				NLogXmlElement nlogXmlElement = new NLogXmlElement(reader);
				if (!string.IsNullOrEmpty(fileName))
				{
					InternalLogger.Info<string>("Configuring from an XML element in {0}...", fileName);
					this.ParseTopLevel(nlogXmlElement, fileName, false);
				}
				else
				{
					this.ParseTopLevel(nlogXmlElement, null, false);
				}
				this.InitializeSucceeded = new bool?(true);
				this.CheckParsingErrors(nlogXmlElement);
				base.CheckUnusedTargets();
			}
			catch (Exception ex)
			{
				this.InitializeSucceeded = new bool?(false);
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
				NLogConfigurationException ex2 = new NLogConfigurationException(ex, "Exception when parsing {0}. ", new object[] { fileName });
				InternalLogger.Error(ex2, "Parsing configuration from {0} failed.", new object[] { fileName });
				if (!ignoreErrors && ex2.MustBeRethrown())
				{
					throw ex2;
				}
			}
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x00033448 File Offset: 0x00031648
		private void CheckParsingErrors(NLogXmlElement rootContentElement)
		{
			string[] array = rootContentElement.GetParsingErrors().ToArray<string>();
			if (array.Any<string>())
			{
				if (LogManager.ThrowConfigExceptions ?? LogManager.ThrowExceptions)
				{
					throw new NLogConfigurationException(string.Join(Environment.NewLine, array));
				}
				foreach (string text in array)
				{
					InternalLogger.Log(LogLevel.Warn, text);
				}
			}
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x000334BA File Offset: 0x000316BA
		private void ConfigureFromFile(string fileName, bool autoReloadDefault)
		{
			if (!this._fileMustAutoReloadLookup.ContainsKey(XmlLoggingConfiguration.GetFileLookupKey(fileName)))
			{
				this.ParseTopLevel(new NLogXmlElement(fileName), fileName, autoReloadDefault);
			}
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x000334E0 File Offset: 0x000316E0
		private void ParseTopLevel(NLogXmlElement content, string filePath, bool autoReloadDefault)
		{
			content.AssertName(new string[] { "nlog", "configuration" });
			string text = content.LocalName.ToUpperInvariant();
			if (text == "CONFIGURATION")
			{
				this.ParseConfigurationElement(content, filePath, autoReloadDefault);
				return;
			}
			if (!(text == "NLOG"))
			{
				return;
			}
			this.ParseNLogElement(content, filePath, autoReloadDefault);
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00033544 File Offset: 0x00031744
		private void ParseConfigurationElement(NLogXmlElement configurationElement, string filePath, bool autoReloadDefault)
		{
			InternalLogger.Trace("ParseConfigurationElement");
			configurationElement.AssertName(new string[] { "configuration" });
			foreach (NLogXmlElement nlogXmlElement in configurationElement.Elements("nlog").ToList<NLogXmlElement>())
			{
				this.ParseNLogElement(nlogXmlElement, filePath, autoReloadDefault);
			}
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x000335C4 File Offset: 0x000317C4
		private void ParseNLogElement(ILoggingConfigurationElement nlogElement, string filePath, bool autoReloadDefault)
		{
			InternalLogger.Trace("ParseNLogElement");
			nlogElement.AssertName(new string[] { "nlog" });
			bool optionalBooleanValue = nlogElement.GetOptionalBooleanValue("autoReload", autoReloadDefault);
			if (!string.IsNullOrEmpty(filePath))
			{
				this._fileMustAutoReloadLookup[XmlLoggingConfiguration.GetFileLookupKey(filePath)] = optionalBooleanValue;
			}
			try
			{
				this._currentFilePath.Push(filePath);
				base.LoadConfig(nlogElement, Path.GetDirectoryName(filePath));
			}
			finally
			{
				this._currentFilePath.Pop();
			}
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00033650 File Offset: 0x00031850
		protected override bool ParseNLogSection(ILoggingConfigurationElement configSection)
		{
			if (configSection.MatchesName("include"))
			{
				string text = this._currentFilePath.Peek();
				bool flag = !string.IsNullOrEmpty(text) && this._fileMustAutoReloadLookup[XmlLoggingConfiguration.GetFileLookupKey(text)];
				this.ParseIncludeElement(configSection, (!string.IsNullOrEmpty(text)) ? Path.GetDirectoryName(text) : null, flag);
				return true;
			}
			return base.ParseNLogSection(configSection);
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x000336B8 File Offset: 0x000318B8
		private void ParseIncludeElement(ILoggingConfigurationElement includeElement, string baseDirectory, bool autoReloadDefault)
		{
			includeElement.AssertName(new string[] { "include" });
			string text = includeElement.GetRequiredValue("file", "nlog");
			bool optionalBooleanValue = includeElement.GetOptionalBooleanValue("ignoreErrors", false);
			try
			{
				text = base.ExpandSimpleVariables(text);
				text = SimpleLayout.Evaluate(text);
				string text2 = text;
				if (baseDirectory != null)
				{
					text2 = Path.Combine(baseDirectory, text);
				}
				if (File.Exists(text2))
				{
					InternalLogger.Debug<string>("Including file '{0}'", text2);
					this.ConfigureFromFile(text2, autoReloadDefault);
				}
				else if (text.Contains("*"))
				{
					this.ConfigureFromFilesByMask(baseDirectory, text, autoReloadDefault);
				}
				else
				{
					if (!optionalBooleanValue)
					{
						throw new FileNotFoundException("Included file not found: " + text2);
					}
					InternalLogger.Debug<string>("Skipping included file '{0}' as it can't be found", text2);
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "Error when including '{0}'.", new object[] { text });
				if (!optionalBooleanValue)
				{
					if (ex.MustBeRethrown())
					{
						throw;
					}
					throw new NLogConfigurationException("Error when including: " + text, ex);
				}
			}
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x000337B4 File Offset: 0x000319B4
		private void ConfigureFromFilesByMask(string baseDirectory, string fileMask, bool autoReloadDefault)
		{
			string text = baseDirectory;
			if (Path.IsPathRooted(fileMask))
			{
				text = Path.GetDirectoryName(fileMask);
				if (text == null)
				{
					InternalLogger.Warn<string>("directory is empty for include of '{0}'", fileMask);
					return;
				}
				string fileName = Path.GetFileName(fileMask);
				if (fileName == null)
				{
					InternalLogger.Warn<string>("filename is empty for include of '{0}'", fileMask);
					return;
				}
				fileMask = fileName;
			}
			foreach (string text2 in Directory.GetFiles(text, fileMask))
			{
				this.ConfigureFromFile(text2, autoReloadDefault);
			}
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x0003381E File Offset: 0x00031A1E
		private static string GetFileLookupKey(string fileName)
		{
			return Path.GetFullPath(fileName);
		}

		// Token: 0x0400050A RID: 1290
		private readonly Dictionary<string, bool> _fileMustAutoReloadLookup;

		// Token: 0x0400050B RID: 1291
		private string _originalFileName;

		// Token: 0x0400050C RID: 1292
		private readonly Stack<string> _currentFilePath;
	}
}
