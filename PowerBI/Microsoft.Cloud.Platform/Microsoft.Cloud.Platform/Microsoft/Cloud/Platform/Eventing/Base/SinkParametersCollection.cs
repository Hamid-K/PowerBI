using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003B7 RID: 951
	public class SinkParametersCollection : IEnumerable<KeyValuePair<string, string>>, IEnumerable, IEquatable<SinkParametersCollection>
	{
		// Token: 0x06001D5E RID: 7518 RVA: 0x0006FF35 File Offset: 0x0006E135
		public SinkParametersCollection([NotNull] IDictionary<string, string> sinkParameters)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IDictionary<string, string>>(sinkParameters, "sinkParameters");
			this.m_map = new Dictionary<string, string>(sinkParameters);
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x0006FF54 File Offset: 0x0006E154
		public SinkParametersCollection([NotNull] IEnumerable<Pair<string, string>> sinkParameters)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<Pair<string, string>>>(sinkParameters, "sinkParameters");
			this.m_map = new Dictionary<string, string>();
			foreach (Pair<string, string> pair in sinkParameters)
			{
				this.m_map.Add(pair.First, pair.Second);
			}
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x0006FFC8 File Offset: 0x0006E1C8
		public bool Has(string name)
		{
			return this.m_map.ContainsKey(name);
		}

		// Token: 0x1700042D RID: 1069
		public string this[string name]
		{
			get
			{
				return this.m_map[name];
			}
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x0006FFE4 File Offset: 0x0006E1E4
		public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
		{
			return this.m_map.GetEnumerator();
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x0006FFF6 File Offset: 0x0006E1F6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x0006FFFE File Offset: 0x0006E1FE
		public int Count
		{
			get
			{
				return this.m_map.Count;
			}
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x0007000C File Offset: 0x0006E20C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, string> keyValuePair in this.m_map)
			{
				stringBuilder.AppendFormat("{0}={1},", keyValuePair.Key, keyValuePair.Value);
			}
			if (this.m_map.Count > 0)
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x0007009C File Offset: 0x0006E29C
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SinkParametersCollection);
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x000700AC File Offset: 0x0006E2AC
		public override int GetHashCode()
		{
			int num = 0;
			foreach (KeyValuePair<string, string> keyValuePair in this.m_map)
			{
				num ^= keyValuePair.Key.GetHashCode() ^ keyValuePair.Value.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x00070118 File Offset: 0x0006E318
		public bool Equals(SinkParametersCollection other)
		{
			return other != null && this.m_map.Count == other.m_map.Count && this.m_map.Intersect(other.m_map).Count<KeyValuePair<string, string>>() == this.m_map.Count;
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001D69 RID: 7529 RVA: 0x00070167 File Offset: 0x0006E367
		public static SinkParametersCollection Empty
		{
			get
			{
				return SinkParametersCollection.sm_empty;
			}
		}

		// Token: 0x04000A01 RID: 2561
		private readonly Dictionary<string, string> m_map;

		// Token: 0x04000A02 RID: 2562
		private static readonly SinkParametersCollection sm_empty = new SinkParametersCollection(new Dictionary<string, string>());
	}
}
