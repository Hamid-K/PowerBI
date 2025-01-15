using System;
using Microsoft.Data.Mashup;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x0200001A RID: 26
	internal class MashupDiscoveryKey
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003B2B File Offset: 0x00001D2B
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00003B33 File Offset: 0x00001D33
		public MashupDiscovery discovery { get; private set; }

		// Token: 0x06000060 RID: 96 RVA: 0x00003B3C File Offset: 0x00001D3C
		public MashupDiscoveryKey(MashupDiscovery discovery)
		{
			this.discovery = discovery;
			this.unknownFunctionName = ((discovery.Kind == MashupDiscoveryKind.UnknownFunction) ? discovery.FunctionName : null);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003B63 File Offset: 0x00001D63
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MashupDiscoveryKey);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003B74 File Offset: 0x00001D74
		public bool Equals(MashupDiscoveryKey other)
		{
			return other != null && this.discovery.Kind == other.discovery.Kind && this.discovery.HasUnknownOptions == other.discovery.HasUnknownOptions && MashupDiscoveryKey.AreEqual<DataSourceReference>(this.discovery.DataSourceReference, other.discovery.DataSourceReference) && MashupDiscoveryKey.AreEqual<string>(this.discovery.Options, other.discovery.Options) && MashupDiscoveryKey.AreEqual<string>(this.unknownFunctionName, other.unknownFunctionName);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003C04 File Offset: 0x00001E04
		public override int GetHashCode()
		{
			return this.discovery.Kind.GetHashCode() + 37 * MashupDiscoveryKey.GetHashCode<DataSourceReference>(this.discovery.DataSourceReference);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003C3E File Offset: 0x00001E3E
		private static int GetHashCode<T>(T value) where T : class, IEquatable<T>
		{
			if (value != null)
			{
				return value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003C55 File Offset: 0x00001E55
		private static bool AreEqual<T>(T value1, T value2) where T : class, IEquatable<T>
		{
			return (value1 == null && value2 == null) || (value1 != null && value1.Equals(value2));
		}

		// Token: 0x040000AA RID: 170
		private readonly string unknownFunctionName;
	}
}
