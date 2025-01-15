using System;
using Microsoft.MachineLearning.Data;

namespace Microsoft.MachineLearning.Learners
{
	// Token: 0x0200047D RID: 1149
	public sealed class RandomPartitioner : IPartitioner
	{
		// Token: 0x0600180C RID: 6156 RVA: 0x00089C48 File Offset: 0x00087E48
		public RoleMappedData[] GetPartitions(IHostEnvironment env, RoleMappedData data, int count)
		{
			Contracts.Check(count > 0);
			if (count <= 1)
			{
				return new RoleMappedData[] { data };
			}
			IHost host = env.Register("RandomPartitioner");
			RoleMappedData[] array2;
			using (IChannel channel = host.Start("Partitioning data"))
			{
				RoleMappedData[] array = new RoleMappedData[count];
				string tempColumnName = data.Data.Schema.GetTempColumnName(null);
				IDataTransform dataTransform = new GenerateNumberTransform(new GenerateNumberTransform.Arguments
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
				for (int i = 0; i < count; i++)
				{
					RangeFilter rangeFilter = new RangeFilter(new RangeFilter.Arguments
					{
						column = tempColumnName,
						min = new double?((double)i / (double)count),
						max = new double?((double)(i + 1) / (double)count)
					}, host, dataTransform);
					RoleMappedData roleMappedData = RoleMappedData.Create(rangeFilter, data.Schema.GetColumnRoleNames());
					array[i] = roleMappedData;
				}
				channel.Done();
				array2 = array;
			}
			return array2;
		}
	}
}
