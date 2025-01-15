using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.MonitoredUtils;
using Microsoft.Cloud.Platform.Security;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Application
{
	// Token: 0x020003EE RID: 1006
	public abstract class ElementBlock : Block
	{
		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x00073DAD File Offset: 0x00071FAD
		protected IActivityFactory ActivityFactory
		{
			get
			{
				return this.m_activityFactory;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001EF3 RID: 7923 RVA: 0x00073DB5 File Offset: 0x00071FB5
		protected IMonitoredActivityCompletionModelFactory MonitoredActivityCompletionModelFactory
		{
			get
			{
				return this.m_monitoredActivityCompletionModelFactory;
			}
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x00073DBD File Offset: 0x00071FBD
		protected ElementBlock(string name)
			: base(name)
		{
			this.m_initializeState = ElementBlock.InitializeState.BeforeInitializeInvoked;
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x00073DD0 File Offset: 0x00071FD0
		protected override BlockInitializationStatus OnInitialize()
		{
			if (this.m_initializeState < ElementBlock.InitializeState.BaseDone)
			{
				this.m_initializeState = ElementBlock.InitializeState.BaseInitializing;
				if (base.OnInitialize() == BlockInitializationStatus.PartiallyDone)
				{
					return BlockInitializationStatus.PartiallyDone;
				}
				this.m_initializeState = ElementBlock.InitializeState.BaseDone;
			}
			else if (this.m_initializeState == ElementBlock.InitializeState.WereDone)
			{
				return BlockInitializationStatus.Done;
			}
			MethodInfo methodInfo = (from method in this.m_eventsKitFactory.GetType().GetMethods()
				where method.Name == "CreateEventsKit" && method.GetParameters().Length == 0
				select method).FirstOrDefault<MethodInfo>();
			foreach (FieldAttributePair<AutoEventsKitAttribute> fieldAttributePair in AttributesUtils.GetFieldsWithAttribute<AutoEventsKitAttribute>(this))
			{
				FieldInfo field = fieldAttributePair.Field;
				Ensure.IsNull<object>(field.GetValue(this), "Field " + field.Name);
				Type fieldType = field.FieldType;
				object obj = methodInfo.MakeGenericMethod(new Type[] { fieldType }).Invoke(this.m_eventsKitFactory, null);
				field.SetValue(this, obj);
			}
			Ensure.IsNull<IConfigurationManager>(this.m_configurationManager, "m_configurationManager");
			this.m_configurationManager = this.m_configurationManagerFactory.GetConfigurationManager();
			this.SatisfyConfigurationManagerSubscriptionImpl(false);
			this.SatisfySecretManagerSubscriptionImpl(false);
			this.m_initializeState = ElementBlock.InitializeState.WereDone;
			return BlockInitializationStatus.Done;
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x00073F08 File Offset: 0x00072108
		protected void SatisfyConfigurationManagerSubscription()
		{
			this.SatisfyConfigurationManagerSubscriptionImpl(true);
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x00073F11 File Offset: 0x00072111
		protected void SatisfySecretManagerSubscription()
		{
			this.SatisfySecretManagerSubscriptionImpl(true);
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x00073F1A File Offset: 0x0007211A
		protected override void OnStart()
		{
			base.OnStart();
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x00073F24 File Offset: 0x00072124
		protected override void OnStop()
		{
			if (this.m_configurationManagerSubscription != null)
			{
				this.m_configurationManager.Unsubscribe(this.m_configurationManagerSubscription);
				this.m_configurationManagerSubscription = null;
			}
			if (this.m_secretManagerSubscription != null)
			{
				this.m_secretManager.Unsubscribe(this.m_secretManagerSubscription);
				this.m_secretManagerSubscription = null;
			}
			base.OnStop();
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x00073F78 File Offset: 0x00072178
		private void SatisfyConfigurationManagerSubscriptionImpl(bool manually)
		{
			IEnumerable<MethodAttributePair<AutoConfigurationManagerSubscriptionAttribute>> methodsWithAttribute = AttributesUtils.GetMethodsWithAttribute<AutoConfigurationManagerSubscriptionAttribute>(this);
			Ensure.IsTrue(methodsWithAttribute.Count<MethodAttributePair<AutoConfigurationManagerSubscriptionAttribute>>() < 2, "A class derived from ElementBlock (" + base.GetType().Name + ") cannot have more than a single [AutoConfigurationManagerSubscription] members.");
			MethodAttributePair<AutoConfigurationManagerSubscriptionAttribute> methodAttributePair = methodsWithAttribute.FirstOrDefault<MethodAttributePair<AutoConfigurationManagerSubscriptionAttribute>>();
			if (methodAttributePair != null && methodAttributePair.Attribute.SubscribeManually == manually)
			{
				Ensure.IsNull<CcsEventHandler>(this.m_configurationManagerSubscription, "m_configurationManagerSubscription");
				this.m_configurationManagerSubscription = Delegate.CreateDelegate(typeof(CcsEventHandler), this, methodAttributePair.Method) as CcsEventHandler;
				Ensure.IsNotNull<CcsEventHandler>(this.m_configurationManagerSubscription, "m_configurationManagerSubscription");
				this.m_configurationManager.Subscribe(methodAttributePair.Attribute.GetConfigurationTypes(), this.m_configurationManagerSubscription);
			}
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x00074028 File Offset: 0x00072228
		private void SatisfySecretManagerSubscriptionImpl(bool manually)
		{
			IEnumerable<MethodAttributePair<AutoSecretManagerSubscriptionAttribute>> methodsWithAttribute = AttributesUtils.GetMethodsWithAttribute<AutoSecretManagerSubscriptionAttribute>(this);
			Ensure.IsTrue(methodsWithAttribute.Count<MethodAttributePair<AutoSecretManagerSubscriptionAttribute>>() < 2, "A class derived from ElementBlock (" + base.GetType().Name + ") cannot have more than a single [AutoSecretManagerSubscription] members.");
			MethodAttributePair<AutoSecretManagerSubscriptionAttribute> methodAttributePair = methodsWithAttribute.FirstOrDefault<MethodAttributePair<AutoSecretManagerSubscriptionAttribute>>();
			if (methodAttributePair != null && methodAttributePair.Attribute.SubscribeManually == manually)
			{
				Ensure.IsNull<SecretManagerEventHandler>(this.m_secretManagerSubscription, "m_secretManagerSubscription");
				this.m_secretManagerSubscription = Delegate.CreateDelegate(typeof(SecretManagerEventHandler), this, methodAttributePair.Method) as SecretManagerEventHandler;
				Ensure.IsNotNull<SecretManagerEventHandler>(this.m_secretManagerSubscription, "m_secretManagerSubscription");
				this.m_secretManager.Subscribe(methodAttributePair.Attribute.GetKeys(), this.m_secretManagerSubscription);
			}
		}

		// Token: 0x04000AE4 RID: 2788
		[BlockServiceDependency]
		private IActivityFactory m_activityFactory;

		// Token: 0x04000AE5 RID: 2789
		[BlockServiceDependency]
		private IMonitoredActivityCompletionModelFactory m_monitoredActivityCompletionModelFactory;

		// Token: 0x04000AE6 RID: 2790
		[BlockServiceDependency]
		private IEventsKitFactory m_eventsKitFactory;

		// Token: 0x04000AE7 RID: 2791
		[BlockServiceDependency]
		private IConfigurationManagerFactory m_configurationManagerFactory;

		// Token: 0x04000AE8 RID: 2792
		private IConfigurationManager m_configurationManager;

		// Token: 0x04000AE9 RID: 2793
		private CcsEventHandler m_configurationManagerSubscription;

		// Token: 0x04000AEA RID: 2794
		[BlockServiceDependency]
		private ISecretManager m_secretManager;

		// Token: 0x04000AEB RID: 2795
		private SecretManagerEventHandler m_secretManagerSubscription;

		// Token: 0x04000AEC RID: 2796
		private ElementBlock.InitializeState m_initializeState;

		// Token: 0x020007F0 RID: 2032
		private enum InitializeState
		{
			// Token: 0x040017C1 RID: 6081
			BeforeInitializeInvoked,
			// Token: 0x040017C2 RID: 6082
			BaseInitializing,
			// Token: 0x040017C3 RID: 6083
			BaseDone,
			// Token: 0x040017C4 RID: 6084
			WereDone
		}

		// Token: 0x020007F1 RID: 2033
		public class ElementBlockDependencies<TEventsKit>
		{
			// Token: 0x1700077E RID: 1918
			// (get) Token: 0x06003226 RID: 12838 RVA: 0x000A9413 File Offset: 0x000A7613
			// (set) Token: 0x06003227 RID: 12839 RVA: 0x000A941B File Offset: 0x000A761B
			public IActivityFactory ActivityFactory { get; protected set; }

			// Token: 0x1700077F RID: 1919
			// (get) Token: 0x06003228 RID: 12840 RVA: 0x000A9424 File Offset: 0x000A7624
			// (set) Token: 0x06003229 RID: 12841 RVA: 0x000A942C File Offset: 0x000A762C
			public IMonitoredActivityCompletionModelFactory MonitoredActivityCompletionModelFactory { get; protected set; }

			// Token: 0x17000780 RID: 1920
			// (get) Token: 0x0600322A RID: 12842 RVA: 0x000A9435 File Offset: 0x000A7635
			// (set) Token: 0x0600322B RID: 12843 RVA: 0x000A943D File Offset: 0x000A763D
			public IWorkTicketFactory WorkTicketManager { get; protected set; }

			// Token: 0x0600322C RID: 12844 RVA: 0x000A9446 File Offset: 0x000A7646
			public ElementBlockDependencies(ElementBlock block)
			{
				this.ActivityFactory = block.ActivityFactory;
				this.MonitoredActivityCompletionModelFactory = block.MonitoredActivityCompletionModelFactory;
				this.WorkTicketManager = block.WorkTicketManager;
			}
		}
	}
}
