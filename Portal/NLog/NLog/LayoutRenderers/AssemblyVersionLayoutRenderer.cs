using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000B4 RID: 180
	[LayoutRenderer("assembly-version")]
	[ThreadAgnostic]
	[ThreadSafe]
	public class AssemblyVersionLayoutRenderer : LayoutRenderer
	{
		// Token: 0x06000B95 RID: 2965 RVA: 0x0001E492 File Offset: 0x0001C692
		public AssemblyVersionLayoutRenderer()
		{
			this.Type = AssemblyVersionType.Assembly;
			this.Format = "major.minor.build.revision";
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0001E4AC File Offset: 0x0001C6AC
		// (set) Token: 0x06000B97 RID: 2967 RVA: 0x0001E4B4 File Offset: 0x0001C6B4
		[DefaultParameter]
		public string Name { get; set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0001E4BD File Offset: 0x0001C6BD
		// (set) Token: 0x06000B99 RID: 2969 RVA: 0x0001E4C5 File Offset: 0x0001C6C5
		[DefaultValue("Assembly")]
		public AssemblyVersionType Type { get; set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0001E4CE File Offset: 0x0001C6CE
		// (set) Token: 0x06000B9B RID: 2971 RVA: 0x0001E4D6 File Offset: 0x0001C6D6
		[DefaultValue("major.minor.build.revision")]
		public string Format
		{
			get
			{
				return this._format;
			}
			set
			{
				this._format = ((value != null) ? value.ToLowerInvariant() : null);
			}
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0001E4EA File Offset: 0x0001C6EA
		protected override void InitializeLayoutRenderer()
		{
			this._assemblyVersion = null;
			base.InitializeLayoutRenderer();
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0001E4F9 File Offset: 0x0001C6F9
		protected override void CloseLayoutRenderer()
		{
			this._assemblyVersion = null;
			base.CloseLayoutRenderer();
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0001E508 File Offset: 0x0001C708
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			string text;
			if ((text = this._assemblyVersion) == null)
			{
				text = (this._assemblyVersion = this.ApplyFormatToVersion(this.GetVersion()));
			}
			string text2 = text;
			if (string.IsNullOrEmpty(text2))
			{
				text2 = string.Format("Could not find value for {0} assembly and version type {1}", string.IsNullOrEmpty(this.Name) ? "entry" : this.Name, this.Type);
			}
			builder.Append(text2);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0001E578 File Offset: 0x0001C778
		private string ApplyFormatToVersion(string version)
		{
			if (this.Format.Equals("major.minor.build.revision", StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(version))
			{
				return version;
			}
			string[] array = version.SplitAndTrimTokens('.');
			version = this.Format.Replace("major", array[0]).Replace("minor", (array.Length > 1) ? array[1] : "0").Replace("build", (array.Length > 2) ? array[2] : "0")
				.Replace("revision", (array.Length > 3) ? array[3] : "0");
			return version;
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0001E610 File Offset: 0x0001C810
		private string GetVersion()
		{
			Assembly assembly = this.GetAssembly();
			return this.GetVersion(assembly);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0001E62B File Offset: 0x0001C82B
		protected virtual Assembly GetAssembly()
		{
			if (string.IsNullOrEmpty(this.Name))
			{
				return Assembly.GetEntryAssembly();
			}
			return Assembly.Load(new AssemblyName(this.Name));
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0001E650 File Offset: 0x0001C850
		private string GetVersion(Assembly assembly)
		{
			AssemblyVersionType type = this.Type;
			if (type != AssemblyVersionType.File)
			{
				if (type != AssemblyVersionType.Informational)
				{
					if (assembly == null)
					{
						return null;
					}
					Version version = assembly.GetName().Version;
					if (version == null)
					{
						return null;
					}
					return version.ToString();
				}
				else
				{
					if (assembly == null)
					{
						return null;
					}
					AssemblyInformationalVersionAttribute customAttribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
					if (customAttribute == null)
					{
						return null;
					}
					return customAttribute.InformationalVersion;
				}
			}
			else
			{
				if (assembly == null)
				{
					return null;
				}
				AssemblyFileVersionAttribute customAttribute2 = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();
				if (customAttribute2 == null)
				{
					return null;
				}
				return customAttribute2.Version;
			}
		}

		// Token: 0x040002CD RID: 717
		private const string DefaultFormat = "major.minor.build.revision";

		// Token: 0x040002CE RID: 718
		private string _format;

		// Token: 0x040002CF RID: 719
		private string _assemblyVersion;
	}
}
