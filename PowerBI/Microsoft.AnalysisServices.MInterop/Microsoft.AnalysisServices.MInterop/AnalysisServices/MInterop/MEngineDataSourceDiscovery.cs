using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Mashup;
using Microsoft.Data.Mashup.Preview;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x0200001B RID: 27
	internal class MEngineDataSourceDiscovery : IMEngineDataSourceDiscovery
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00003C7F File Offset: 0x00001E7F
		public MEngineDataSourceDiscovery(string MProgram, MEngineDiscoveryOptions discoveryOptions)
		{
			this.MProgram = MProgram;
			this.discoveryOptions = discoveryOptions;
			this.OnInit();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003C9C File Offset: 0x00001E9C
		protected virtual void OnInit()
		{
			MashupConnectionStringBuilder mashupConnectionStringBuilder = new MashupConnectionStringBuilder
			{
				Mashup = this.MProgram
			};
			this.connection = new MashupConnection(mashupConnectionStringBuilder.ConnectionString);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003CCC File Offset: 0x00001ECC
		public virtual IEnumerable<MashupDiscovery> FindReferencedDataSources(string memberName)
		{
			HashSet<MashupDiscoveryKey> hashSet = new HashSet<MashupDiscoveryKey>();
			foreach (MashupDiscovery mashupDiscovery in this.connection.FindReferencedDataSources(this.GetMashupDiscoveryOptions()))
			{
				hashSet.Add(new MashupDiscoveryKey(mashupDiscovery));
			}
			return hashSet.Select((MashupDiscoveryKey key) => key.discovery);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003D58 File Offset: 0x00001F58
		public virtual ISet<MEngineLibrarySymbol> FindReferencedLibrarySymbols(string memberName)
		{
			HashSet<MEngineLibrarySymbol> hashSet = new HashSet<MEngineLibrarySymbol>();
			foreach (KeyValuePair<MEngineLibrarySymbol, string> keyValuePair in MEngineDataSourceDiscovery.librarySymbolMap)
			{
				if (MashupSourceHelper.FindReferencedLibrarySymbols(this.connection, new string[] { keyValuePair.Value }).Any<string>())
				{
					hashSet.Add(keyValuePair.Key);
				}
			}
			return hashSet;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003DDC File Offset: 0x00001FDC
		protected MashupDiscoveryOptions GetMashupDiscoveryOptions()
		{
			MEngineDiscoveryOptions mengineDiscoveryOptions = this.discoveryOptions;
			if (mengineDiscoveryOptions == MEngineDiscoveryOptions.Default)
			{
				return MashupDiscoveryOptions.ReportOptionsRecord | MashupDiscoveryOptions.IgnoreNativeQueries;
			}
			if (mengineDiscoveryOptions != MEngineDiscoveryOptions.ForTridentDataSource)
			{
				throw MInteropHelperImpl.InternalError("D:\\dbs\\sh\\uikp\\0709_010605\\cmd\\13\\Sql\\Picasso\\Engine\\src\\om\\MInterop\\MEngineDataSourceDiscovery.cs", "GetMashupDiscoveryOptions", 97);
			}
			return MashupDiscoveryOptions.ReportExtensionDSR | MashupDiscoveryOptions.ReportNavigationSteps | MashupDiscoveryOptions.IgnoreNativeQueries;
		}

		// Token: 0x040000AB RID: 171
		protected static Dictionary<MEngineLibrarySymbol, string> librarySymbolMap = new Dictionary<MEngineLibrarySymbol, string>
		{
			{
				MEngineLibrarySymbol.WebPage,
				"Web.Page"
			},
			{
				MEngineLibrarySymbol.PdfTables,
				"Pdf.Tables"
			},
			{
				MEngineLibrarySymbol.ExcelWorkbook,
				"Excel.Workbook"
			}
		};

		// Token: 0x040000AC RID: 172
		protected MashupConnection connection;

		// Token: 0x040000AD RID: 173
		protected string MProgram;

		// Token: 0x040000AE RID: 174
		protected MEngineDiscoveryOptions discoveryOptions;
	}
}
