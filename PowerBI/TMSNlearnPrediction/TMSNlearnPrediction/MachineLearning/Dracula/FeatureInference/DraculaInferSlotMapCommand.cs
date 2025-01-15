using System;
using System.Linq;
using Microsoft.MachineLearning.Command;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data;

namespace Microsoft.MachineLearning.Dracula.FeatureInference
{
	// Token: 0x02000415 RID: 1045
	public sealed class DraculaInferSlotMapCommand : ICommand
	{
		// Token: 0x060015DE RID: 5598 RVA: 0x0007F9AC File Offset: 0x0007DBAC
		public DraculaInferSlotMapCommand(DraculaInferSlotMapCommand.Arguments args, IHostEnvironment env)
		{
			this._impl = new DraculaInferSlotMapCommand.Impl(args, env);
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x0007F9C1 File Offset: 0x0007DBC1
		public void Run()
		{
			this._impl.Run();
		}

		// Token: 0x04000D69 RID: 3433
		internal const string Summary = "Tries to find the combinations of slots that are giving the best performance for a Dracula transform.";

		// Token: 0x04000D6A RID: 3434
		private readonly DraculaInferSlotMapCommand.Impl _impl;

		// Token: 0x02000416 RID: 1046
		public sealed class Arguments : DataCommand.ArgumentsBase
		{
			// Token: 0x04000D6B RID: 3435
			[Argument(4, HelpText = "Slot map builder", ShortName = "b")]
			public SubComponent<IDraculaSlotMapBuilder, SignatureDraculaSlotMapBuilder> builder;
		}

		// Token: 0x02000417 RID: 1047
		private sealed class Impl : DataCommand.ImplBase<DraculaInferSlotMapCommand.Arguments>
		{
			// Token: 0x060015E1 RID: 5601 RVA: 0x0007F9D8 File Offset: 0x0007DBD8
			public Impl(DraculaInferSlotMapCommand.Arguments args, IHostEnvironment env)
				: base("DraculaInferSlotMapCommand", args, env, null)
			{
				Contracts.CheckUserArg(this._host, SubComponentExtensions.IsGood(args.builder), "builder", "Builder must be specified");
			}

			// Token: 0x060015E2 RID: 5602 RVA: 0x0007FA1C File Offset: 0x0007DC1C
			public override void Run()
			{
				IDataLoader dataLoader = base.CreateAndSaveLoader("TextLoader");
				IDraculaSlotMapBuilder draculaSlotMapBuilder = ComponentCatalog.CreateInstance<IDraculaSlotMapBuilder, SignatureDraculaSlotMapBuilder>(this._args.builder, new object[] { this._host, dataLoader });
				using (IChannel channel = this._host.Start("Infer columns"))
				{
					string text = this.GenerateSlotMapString(draculaSlotMapBuilder.CreateSlotMap());
					channel.Info("Suggested custom slot map:");
					channel.Info(text);
					channel.Done();
				}
			}

			// Token: 0x060015E3 RID: 5603 RVA: 0x0007FAC1 File Offset: 0x0007DCC1
			private string GenerateSlotMapString(int[][] slotMap)
			{
				return string.Join(";", slotMap.Select((int[] s) => string.Join<int>(",", s)));
			}
		}
	}
}
