using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000011 RID: 17
	internal static class Repository
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x00004370 File Offset: 0x00002570
		public static RfcCustomRepository DeserializeCustomRepositoryOrNull(string resourceFile)
		{
			try
			{
				using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceFile))
				{
					if (manifestResourceStream != null)
					{
						using (StreamReader streamReader = new StreamReader(manifestResourceStream))
						{
							RfcCustomRepository rfcCustomRepository = new RfcCustomRepository();
							rfcCustomRepository.Load(streamReader);
							return rfcCustomRepository;
						}
					}
				}
			}
			catch (RfcSerializationException)
			{
			}
			return null;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000043E8 File Offset: 0x000025E8
		public static RfcCustomRepository BuildCustomRepository(IEnumerable<string> functionNames, IDestination destination)
		{
			RfcCustomRepository rfcCustomRepository = new RfcCustomRepository();
			HashSet<string> hashSet = new HashSet<string>();
			foreach (string text in new HashSet<string>(functionNames))
			{
				IRfcFunction rfcFunction = destination.CreateFunction(text);
				if (rfcFunction.Metadata != null)
				{
					rfcCustomRepository.AddFunctionMetadata(rfcFunction.Metadata);
					hashSet.Add(rfcFunction.Metadata.Name);
					for (int i = 0; i < rfcFunction.Metadata.ParameterCount; i++)
					{
						RfcDataType dataType = rfcFunction.GetElementMetadata(i).DataType;
						if (dataType != 24)
						{
							if (dataType == 25)
							{
								IRfcTable table = rfcFunction.GetTable(i);
								if (table.Metadata != null)
								{
									if (!hashSet.Contains(table.Metadata.Name))
									{
										rfcCustomRepository.AddTableMetadata(table.Metadata);
										hashSet.Add(table.Metadata.Name);
									}
									if (table.Metadata.LineType != null && !hashSet.Contains(table.Metadata.LineType.Name))
									{
										rfcCustomRepository.AddStructureMetadata(table.Metadata.LineType);
										hashSet.Add(table.Metadata.LineType.Name);
									}
								}
							}
						}
						else
						{
							IRfcStructure structure = rfcFunction.GetStructure(i);
							if (structure.Metadata != null && !hashSet.Contains(structure.Metadata.Name))
							{
								rfcCustomRepository.AddStructureMetadata(structure.Metadata);
								hashSet.Add(structure.Metadata.Name);
							}
						}
					}
				}
			}
			return rfcCustomRepository;
		}
	}
}
