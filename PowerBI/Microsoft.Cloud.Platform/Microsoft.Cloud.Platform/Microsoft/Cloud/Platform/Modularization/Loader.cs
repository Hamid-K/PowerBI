using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000CB RID: 203
	public static class Loader
	{
		// Token: 0x060005CA RID: 1482 RVA: 0x00014B04 File Offset: 0x00012D04
		public static IList<IBlock> LoadConfiguredBlocks()
		{
			List<IBlock> blocks = new List<IBlock>();
			UtilsContext.Current.RunWithClearContext(delegate
			{
				ApplicationRootConfiguration applicationRootConfiguration = ConfigurationManager.GetSection("ApplicationRoot") as ApplicationRootConfiguration;
				if (applicationRootConfiguration == null)
				{
					throw new ConfigurationErrorsException("Cannot find configuration section 'applicationRoot'");
				}
				BlocksConfiguration blocksConfiguration = applicationRootConfiguration.BlocksConfiguration;
				if (blocksConfiguration == null)
				{
					throw new ConfigurationErrorsException("Cannot find configuration element 'applicationRoot/blocks");
				}
				foreach (object obj in blocksConfiguration)
				{
					BlockConfiguration blockConfiguration = (BlockConfiguration)obj;
					string assembly = blockConfiguration.Assembly;
					try
					{
						IBlock block = (IBlock)DynamicLoader.Instantiate(assembly, blockConfiguration.Type, new Predicate<Type>(DynamicLoader.ImplementsInterface<IBlock>), LoadOptions.None, new object[] { blockConfiguration.Name });
						blocks.Add(block);
					}
					catch (DynamicLoaderException ex)
					{
						throw new ConfigurationErrorsException(string.Format(CultureInfo.CurrentCulture, "Cannot create an instance of type '{0}' from assembly '{1}'.", new object[] { blockConfiguration.Type, assembly }), ex);
					}
				}
			});
			return blocks;
		}
	}
}
