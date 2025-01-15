using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000E1 RID: 225
	[Serializable]
	internal class DeploymentSettingsElement : ConfigurationElement, ISerializable
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x00019DEC File Offset: 0x00017FEC
		public static string Name
		{
			get
			{
				return "deploymentSettings";
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00015607 File Offset: 0x00013807
		internal DeploymentSettingsElement()
		{
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00019DF4 File Offset: 0x00017FF4
		// (set) Token: 0x06000677 RID: 1655 RVA: 0x00019E21 File Offset: 0x00018021
		[ConfigurationProperty("deploymentMode", IsRequired = false)]
		public DeploymentModeElement DeploymentMode
		{
			get
			{
				DeploymentModeElement deploymentModeElement = (DeploymentModeElement)base["deploymentMode"];
				if (deploymentModeElement.Value == DataCacheDeploymentMode.Unknown)
				{
					return DeploymentSettingsElement.RoutingModeElement;
				}
				return deploymentModeElement;
			}
			set
			{
				base["deploymentMode"] = value;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x00019E2F File Offset: 0x0001802F
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x00019E41 File Offset: 0x00018041
		[ConfigurationProperty("gracefulShutdown", IsRequired = false)]
		public GracefulShutdownElement GracefulShutdown
		{
			get
			{
				return (GracefulShutdownElement)base["gracefulShutdown"];
			}
			set
			{
				base["gracefulShutdown"] = value;
			}
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00019E50 File Offset: 0x00018050
		protected DeploymentSettingsElement(SerializationInfo info, StreamingContext context)
		{
			this.DeploymentMode = (DeploymentModeElement)info.GetValue("deploymentMode", typeof(DeploymentModeElement));
			try
			{
				this.GracefulShutdown = (GracefulShutdownElement)info.GetValue("gracefulShutdown", typeof(GracefulShutdownElement));
			}
			catch (SerializationException)
			{
				this.GracefulShutdown = new GracefulShutdownElement();
			}
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00019EC4 File Offset: 0x000180C4
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("deploymentMode", this.DeploymentMode);
			info.AddValue("gracefulShutdown", this.GracefulShutdown);
		}

		// Token: 0x040003E9 RID: 1001
		internal const string DEPLOYMENT_MODE = "deploymentMode";

		// Token: 0x040003EA RID: 1002
		internal const string GRACEFUL_SHUTDOWN = "gracefulShutdown";

		// Token: 0x040003EB RID: 1003
		public static DeploymentModeElement RoutingModeElement = new DeploymentModeElement(DataCacheDeploymentMode.RoutingClient);
	}
}
