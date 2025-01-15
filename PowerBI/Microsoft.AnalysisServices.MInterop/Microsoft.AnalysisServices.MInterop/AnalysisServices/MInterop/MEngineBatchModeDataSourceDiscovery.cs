using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AnalysisServices.PlatformHost;
using Microsoft.Data.Mashup;
using Microsoft.Data.Mashup.Preview;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x02000019 RID: 25
	internal class MEngineBatchModeDataSourceDiscovery : MEngineDataSourceDiscovery
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00003654 File Offset: 0x00001854
		public MEngineBatchModeDataSourceDiscovery(string MProgram, MEngineDiscoveryOptions discoveryOptions)
			: base(MProgram, discoveryOptions)
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000368C File Offset: 0x0000188C
		protected override void OnInit()
		{
			string text;
			this.MProgram = MEngineBatchModeDataSourceDiscovery.ComputeFullyMinimizedMProgram(this.MProgram, out text);
			base.OnInit();
			Dictionary<string, string[]> queryDependencies = MHelper.GetQueryDependencies(this.MProgram);
			foreach (KeyValuePair<string, string[]> keyValuePair in queryDependencies)
			{
				string text2 = MHelper.EscapeIdentifier(keyValuePair.Key);
				this.memberDiscoveries.Add(text2, new HashSet<MashupDiscoveryKey>());
				this.memberSymbols.Add(text2, new HashSet<MEngineLibrarySymbol>());
			}
			foreach (MashupDiscovery mashupDiscovery in this.connection.FindReferencedDataSources(base.GetMashupDiscoveryOptions(), MashupPartitionCoordinateType.Member))
			{
				string text3 = MHelper.EscapeIdentifier(mashupDiscovery.Coordinate.MemberName);
				this.memberDiscoveries[text3].Add(new MashupDiscoveryKey(mashupDiscovery));
			}
			foreach (KeyValuePair<MEngineLibrarySymbol, string> keyValuePair2 in MEngineDataSourceDiscovery.librarySymbolMap)
			{
				MEngineLibrarySymbol key = keyValuePair2.Key;
				foreach (MashupDiscovery mashupDiscovery2 in MashupSourceHelper.FindReferencedLibrarySymbols(this.connection, MashupPartitionCoordinateType.Member, new string[] { keyValuePair2.Value }))
				{
					string text4 = MHelper.EscapeIdentifier(mashupDiscovery2.Coordinate.MemberName);
					this.memberSymbols[text4].Add(key);
				}
			}
			queryDependencies.Remove(text);
			foreach (KeyValuePair<string, string[]> keyValuePair3 in queryDependencies)
			{
				string text5 = MHelper.EscapeIdentifier(keyValuePair3.Key);
				string[] value = keyValuePair3.Value;
				for (int i = 0; i < value.Length; i++)
				{
					string text6 = MHelper.EscapeIdentifier(value[i]);
					this.memberDiscoveries[text5].UnionWith(this.memberDiscoveries[text6]);
					this.memberSymbols[text5].UnionWith(this.memberSymbols[text6]);
				}
			}
			IEngineTracer engineTracer = MInteropHelperImpl.EngineTracer;
			if (engineTracer == null)
			{
				return;
			}
			engineTracer.LogPrivateMessage(string.Format("Using batch mode static analysis for MProgram: {0}", this.MProgram.RedactSensitiveStrings().MarkAsCustomerContent()));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000393C File Offset: 0x00001B3C
		public override IEnumerable<MashupDiscovery> FindReferencedDataSources(string memberName)
		{
			if (this.memberDiscoveries.ContainsKey(memberName))
			{
				return this.memberDiscoveries[memberName].Select((MashupDiscoveryKey key) => key.discovery);
			}
			IEngineTracer engineTracer = MInteropHelperImpl.EngineTracer;
			if (engineTracer != null)
			{
				engineTracer.LogPrivateMessage(string.Format("Batch mode static analysis: memberName={0} not found in memberDiscoveries, using simple static analysis", memberName.MarkAsCustomerContent()));
			}
			return this.GetAdhocDiscovery(memberName).FindReferencedDataSources(memberName);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000039B8 File Offset: 0x00001BB8
		public override ISet<MEngineLibrarySymbol> FindReferencedLibrarySymbols(string memberName)
		{
			if (this.memberSymbols.ContainsKey(memberName))
			{
				return this.memberSymbols[memberName];
			}
			IEngineTracer engineTracer = MInteropHelperImpl.EngineTracer;
			if (engineTracer != null)
			{
				engineTracer.LogPrivateMessage(string.Format("Batch mode static analysis: memberName={0} not found in memberSymbols, using simple static analysis", memberName.MarkAsCustomerContent()));
			}
			return this.GetAdhocDiscovery(memberName).FindReferencedLibrarySymbols(memberName);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003A10 File Offset: 0x00001C10
		private IMEngineDataSourceDiscovery GetAdhocDiscovery(string memberName)
		{
			if (this.adhocDataSourceDiscovery.ContainsKey(memberName))
			{
				return this.adhocDataSourceDiscovery[memberName];
			}
			MEngineDataSourceDiscovery mengineDataSourceDiscovery = new MEngineDataSourceDiscovery(this.GetMinimizedMProgram(memberName), this.discoveryOptions);
			this.adhocDataSourceDiscovery.Add(memberName, mengineDataSourceDiscovery);
			return mengineDataSourceDiscovery;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003A5C File Offset: 0x00001C5C
		private string GetMinimizedMProgram(string MQuery)
		{
			if (this.minimizedPrograms.ContainsKey(MQuery))
			{
				return this.minimizedPrograms[MQuery];
			}
			string text;
			MInteropHelperImpl.GetMinimizedPartitionMProgram(this.MProgram, MQuery, out text);
			this.minimizedPrograms.Add(MQuery, text);
			return text;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003AA0 File Offset: 0x00001CA0
		private static string ComputeFullyMinimizedMProgram(string MProgram, out string newMemberName)
		{
			newMemberName = string.Empty;
			IEnumerable<string> enumerable = MHelper.GetSharedMembers(MProgram).Keys.Select((string member) => MHelper.EscapeIdentifier(member));
			if (enumerable.Count<string>() == 0)
			{
				return MProgram;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{ ");
			stringBuilder.Append(string.Join(", ", enumerable));
			stringBuilder.Append(" }");
			return MInteropHelperImpl.GetMinimizedMProgram(MProgram, stringBuilder.ToString(), out newMemberName);
		}

		// Token: 0x040000A5 RID: 165
		private readonly Dictionary<string, HashSet<MashupDiscoveryKey>> memberDiscoveries = new Dictionary<string, HashSet<MashupDiscoveryKey>>();

		// Token: 0x040000A6 RID: 166
		private readonly Dictionary<string, HashSet<MEngineLibrarySymbol>> memberSymbols = new Dictionary<string, HashSet<MEngineLibrarySymbol>>();

		// Token: 0x040000A7 RID: 167
		private readonly Dictionary<string, IMEngineDataSourceDiscovery> adhocDataSourceDiscovery = new Dictionary<string, IMEngineDataSourceDiscovery>();

		// Token: 0x040000A8 RID: 168
		private readonly Dictionary<string, string> minimizedPrograms = new Dictionary<string, string>();
	}
}
