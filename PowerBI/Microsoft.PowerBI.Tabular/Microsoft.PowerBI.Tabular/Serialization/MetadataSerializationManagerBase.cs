using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000186 RID: 390
	public abstract class MetadataSerializationManagerBase : IMetadataSerializationController, IMetadataDeserializationController
	{
		// Token: 0x06001810 RID: 6160 RVA: 0x000A3352 File Offset: 0x000A1552
		private protected MetadataSerializationManagerBase()
		{
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x000A335A File Offset: 0x000A155A
		private protected virtual void OperationStartNotification(bool isSerializing, object context, IReadOnlyCollection<string> logicalPaths)
		{
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x000A335C File Offset: 0x000A155C
		private protected virtual void DocumentStartNotification(bool isSerializing, object context, string logicalPath)
		{
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x000A335E File Offset: 0x000A155E
		private protected virtual void DocumentEndNotification(bool isSerializing, object context, string logicalPath, bool isSuccessful)
		{
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x000A3360 File Offset: 0x000A1560
		private protected virtual void OperationEndNotification(bool isSerializing, object context, bool isSuccessful)
		{
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x000A3362 File Offset: 0x000A1562
		private protected virtual void ErrorNotification(bool isSerializing, object context, Exception e)
		{
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x000A3364 File Offset: 0x000A1564
		private protected virtual void OnSerializationStart(object userContext, IReadOnlyCollection<string> logicalPaths, out object operationContext)
		{
			operationContext = null;
		}

		// Token: 0x06001817 RID: 6167
		private protected abstract void OnDocumentSerializationStart(object userContext, object operationContext, string logicalPath, out object documentContext, out Stream document);

		// Token: 0x06001818 RID: 6168
		private protected abstract void OnDocumentSerializationEnd(object userContext, object operationContext, string logicalPath, object documentContext, Stream document, bool isSuccessfulSerialization);

		// Token: 0x06001819 RID: 6169 RVA: 0x000A3369 File Offset: 0x000A1569
		private protected virtual void OnSerializationEnd(object userContext, object operationContext, bool isSuccessfulSerialization)
		{
		}

		// Token: 0x0600181A RID: 6170
		private protected abstract IReadOnlyCollection<string> GetDocumentsForDeserialization(object userContext);

		// Token: 0x0600181B RID: 6171 RVA: 0x000A336B File Offset: 0x000A156B
		private protected virtual void OnDeserializationStart(object userContext, out object operationContext, out IReadOnlyCollection<string> logicalPaths)
		{
			operationContext = null;
			logicalPaths = this.GetDocumentsForDeserialization(userContext);
		}

		// Token: 0x0600181C RID: 6172
		private protected abstract void OnDocumentDeserializationStart(object userContext, object operationContext, string logicalPath, out object documentContext, out Stream document);

		// Token: 0x0600181D RID: 6173
		private protected abstract void OnDocumentDeserializationEnd(object userContext, object operationContext, string logicalPath, object documentContext, Stream document, bool isSuccessfulDeserialization);

		// Token: 0x0600181E RID: 6174 RVA: 0x000A3379 File Offset: 0x000A1579
		private protected virtual void OnDeserializationEnd(object userContext, object operationContext, bool isSuccessfulDeserialization)
		{
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x000A337B File Offset: 0x000A157B
		void IMetadataSerializationController.OnSerializationStart(object userContext, IReadOnlyCollection<string> logicalPaths, out object operationContext)
		{
			this.StartOperation();
			this.OnSerializationStart(userContext, logicalPaths, out operationContext);
			this.OperationStartNotification(true, userContext, logicalPaths);
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x000A3395 File Offset: 0x000A1595
		void IMetadataSerializationController.OnDocumentSerializationStart(object userContext, object operationContext, string logicalPath, out object documentContext, out Stream document)
		{
			this.EnsureInOperation();
			this.OnDocumentSerializationStart(userContext, operationContext, logicalPath, out documentContext, out document);
			this.DocumentStartNotification(true, userContext, logicalPath);
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x000A33B3 File Offset: 0x000A15B3
		void IMetadataSerializationController.OnDocumentSerializationEnd(object userContext, object operationContext, string logicalPath, object documentContext, Stream document, bool isSuccessfulSerialization)
		{
			this.EnsureInOperation();
			this.DocumentEndNotification(true, userContext, logicalPath, isSuccessfulSerialization);
			this.OnDocumentSerializationEnd(userContext, operationContext, logicalPath, documentContext, document, isSuccessfulSerialization);
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x000A33D5 File Offset: 0x000A15D5
		void IMetadataSerializationController.OnSerializationEnd(object userContext, object operationContext, bool isSuccessfulSerialization)
		{
			this.OperationEndNotification(true, userContext, isSuccessfulSerialization);
			this.OnSerializationEnd(userContext, operationContext, isSuccessfulSerialization);
			this.CompleteOperation();
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x000A33EF File Offset: 0x000A15EF
		void IMetadataSerializationController.OnSerializationError(object userContext, object operationContext, Exception e)
		{
			this.EnsureInOperation();
			this.ErrorNotification(true, userContext, e);
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x000A3400 File Offset: 0x000A1600
		void IMetadataDeserializationController.OnDeserializationStart(object userContext, out object operationContext, out IReadOnlyCollection<string> logicalPaths)
		{
			this.StartOperation();
			this.OnDeserializationStart(userContext, out operationContext, out logicalPaths);
			this.OperationStartNotification(false, userContext, logicalPaths);
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x000A341B File Offset: 0x000A161B
		void IMetadataDeserializationController.OnDocumentDeserializationStart(object userContext, object operationContext, string logicalPath, out object documentContext, out Stream document)
		{
			this.EnsureInOperation();
			this.OnDocumentDeserializationStart(userContext, operationContext, logicalPath, out documentContext, out document);
			this.DocumentStartNotification(false, userContext, logicalPath);
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x000A3439 File Offset: 0x000A1639
		void IMetadataDeserializationController.OnDocumentDeserializationEnd(object userContext, object operationContext, string logicalPath, object documentContext, Stream document, bool isSuccessfulDeserialization)
		{
			this.EnsureInOperation();
			this.DocumentEndNotification(false, userContext, logicalPath, isSuccessfulDeserialization);
			this.OnDocumentDeserializationEnd(userContext, operationContext, logicalPath, documentContext, document, isSuccessfulDeserialization);
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x000A345B File Offset: 0x000A165B
		void IMetadataDeserializationController.OnDeserializationEnd(object userContext, object operationContext, bool isSuccessfulDeserialization)
		{
			this.OperationEndNotification(true, userContext, isSuccessfulDeserialization);
			this.OnDeserializationEnd(userContext, operationContext, isSuccessfulDeserialization);
			this.CompleteOperation();
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x000A3475 File Offset: 0x000A1675
		void IMetadataDeserializationController.OnDeserializationError(object userContext, object operationContext, Exception e)
		{
			this.EnsureInOperation();
			this.ErrorNotification(false, userContext, e);
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x000A3486 File Offset: 0x000A1686
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void StartOperation()
		{
			if (this.isActive)
			{
				throw new InvalidOperationException("There is already an ongoing serialization or deserialization operation in-progress; a new operation cannot be started!");
			}
			this.isActive = true;
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x000A34A2 File Offset: 0x000A16A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void EnsureInOperation()
		{
			if (!this.isActive)
			{
				throw new InvalidOperationException("There is not an ongoing serialization or deserialization operation in-progress!");
			}
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x000A34B7 File Offset: 0x000A16B7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CompleteOperation()
		{
			if (!this.isActive)
			{
				throw new InvalidOperationException("There is not an ongoing serialization or deserialization operation in-progress!");
			}
			this.isActive = false;
		}

		// Token: 0x04000481 RID: 1153
		private bool isActive;
	}
}
