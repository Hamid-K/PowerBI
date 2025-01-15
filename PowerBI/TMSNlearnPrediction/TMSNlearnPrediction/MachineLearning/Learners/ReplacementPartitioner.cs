using System;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data;
using Microsoft.TMSN.TMSNlearn;

namespace Microsoft.MachineLearning.Learners
{
	// Token: 0x0200047E RID: 1150
	public sealed class ReplacementPartitioner : IPartitioner
	{
		// Token: 0x0600180D RID: 6157 RVA: 0x00089D84 File Offset: 0x00087F84
		public ReplacementPartitioner(ReplacementPartitioner.Arguments args)
		{
			Contracts.CheckValue<ReplacementPartitioner.Arguments>(args, "args");
			if (0f >= args.sampleRate || args.sampleRate > 1f)
			{
				throw Contracts.ExceptUserArg("sample rate", "The sample rate must be greater than 0 and less then or equal to 1. Specified sample rate is {0}.", new object[] { args.sampleRate });
			}
			this._sampleRate = args.sampleRate;
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x00089DF0 File Offset: 0x00087FF0
		public RoleMappedData[] GetPartitions(IHostEnvironment env, RoleMappedData data, int count)
		{
			Contracts.Check(count > 0);
			IHost host = env.Register("RandomPartitioner");
			RoleMappedData[] array2;
			using (IChannel channel = host.Start("Partitioning data"))
			{
				RoleMappedData[] array = new RoleMappedData[count];
				if (this._sampleRate == 1f)
				{
					for (int i = 0; i < count; i++)
					{
						array[i] = data;
					}
				}
				else
				{
					string tempColumnName = data.Data.Schema.GetTempColumnName(null);
					for (int j = 0; j < count; j++)
					{
						GenerateNumberTransform generateNumberTransform = new GenerateNumberTransform(new GenerateNumberTransform.Arguments
						{
							column = new GenerateNumberTransform.Column[]
							{
								new GenerateNumberTransform.Column
								{
									name = tempColumnName
								}
							},
							seed = (uint)host.Rand.Next()
						}, host, data.Data);
						RangeFilter rangeFilter = new RangeFilter(new RangeFilter.Arguments
						{
							column = tempColumnName,
							max = new double?((double)this._sampleRate)
						}, host, generateNumberTransform);
						RoleMappedData roleMappedData = RoleMappedData.Create(rangeFilter, data.Schema.GetColumnRoleNames());
						array[j] = roleMappedData;
					}
				}
				channel.Done();
				array2 = array;
			}
			return array2;
		}

		// Token: 0x04000E79 RID: 3705
		private readonly float _sampleRate;

		// Token: 0x0200047F RID: 1151
		public class Arguments
		{
			// Token: 0x04000E7A RID: 3706
			[TGUI(Label = "Sample Rate", Description = "Sample rate for replacement partitioner")]
			[Argument(0, HelpText = "Sample rate for replacement partitioner", ShortName = "sr", SortOrder = 50)]
			public float sampleRate = 0.8f;
		}
	}
}
