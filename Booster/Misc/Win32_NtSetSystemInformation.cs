using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;

namespace Booster.Misc
{
    internal sealed class Win32_NtSetSystemInformation
    {
        [DllImport("ntdll.dll")]
        internal static extern uint NtSetSystemInformation(int infoClass, IntPtr info, int length);
        public void ClearStandbyCache()
        {
            if (Win32_PrivilegeElevation.SetIncreasePrivilege("SeProfileSingleProcessPrivilege"))
            {
                try
                {
                    int systemInfoLength = Marshal.SizeOf<int>(4);
                    GCHandle gcHandle = GCHandle.Alloc(4, GCHandleType.Pinned);
                    if (Win32_NtSetSystemInformation.NtSetSystemInformation(80, gcHandle.AddrOfPinnedObject(), systemInfoLength) != 0U)
                    {
                        throw new Exception("NtSetSystemInformation: ", new Win32Exception(Marshal.GetLastWin32Error()));
                    }
                    gcHandle.Free();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        // Token: 0x04000072 RID: 114
        private const int MemoryPurgeStandbyList = 4;

        // Token: 0x02000028 RID: 40
        internal enum SYSTEM_INFORMATION_CLASS
        {
            // Token: 0x040000B5 RID: 181
            SystemBasicInformation,
            // Token: 0x040000B6 RID: 182
            SystemProcessorInformation,
            // Token: 0x040000B7 RID: 183
            SystemPerformanceInformation,
            // Token: 0x040000B8 RID: 184
            SystemTimeOfDayInformation,
            // Token: 0x040000B9 RID: 185
            SystemPathInformation,
            // Token: 0x040000BA RID: 186
            SystemProcessInformation,
            // Token: 0x040000BB RID: 187
            SystemCallCountInformation,
            // Token: 0x040000BC RID: 188
            SystemDeviceInformation,
            // Token: 0x040000BD RID: 189
            SystemProcessorPerformanceInformation,
            // Token: 0x040000BE RID: 190
            SystemFlagsInformation,
            // Token: 0x040000BF RID: 191
            SystemCallTimeInformation,
            // Token: 0x040000C0 RID: 192
            SystemModuleInformation,
            // Token: 0x040000C1 RID: 193
            SystemLocksInformation,
            // Token: 0x040000C2 RID: 194
            SystemStackTraceInformation,
            // Token: 0x040000C3 RID: 195
            SystemPagedPoolInformation,
            // Token: 0x040000C4 RID: 196
            SystemNonPagedPoolInformation,
            // Token: 0x040000C5 RID: 197
            SystemHandleInformation,
            // Token: 0x040000C6 RID: 198
            SystemObjectInformation,
            // Token: 0x040000C7 RID: 199
            SystemPageFileInformation,
            // Token: 0x040000C8 RID: 200
            SystemVdmInstemulInformation,
            // Token: 0x040000C9 RID: 201
            SystemVdmBopInformation,
            // Token: 0x040000CA RID: 202
            SystemFileCacheInformation,
            // Token: 0x040000CB RID: 203
            SystemPoolTagInformation,
            // Token: 0x040000CC RID: 204
            SystemInterruptInformation,
            // Token: 0x040000CD RID: 205
            SystemDpcBehaviorInformation,
            // Token: 0x040000CE RID: 206
            SystemFullMemoryInformation,
            // Token: 0x040000CF RID: 207
            SystemLoadGdiDriverInformation,
            // Token: 0x040000D0 RID: 208
            SystemUnloadGdiDriverInformation,
            // Token: 0x040000D1 RID: 209
            SystemTimeAdjustmentInformation,
            // Token: 0x040000D2 RID: 210
            SystemSummaryMemoryInformation,
            // Token: 0x040000D3 RID: 211
            SystemMirrorMemoryInformation,
            // Token: 0x040000D4 RID: 212
            SystemPerformanceTraceInformation,
            // Token: 0x040000D5 RID: 213
            SystemCrashDumpInformation,
            // Token: 0x040000D6 RID: 214
            SystemExceptionInformation,
            // Token: 0x040000D7 RID: 215
            SystemCrashDumpStateInformation,
            // Token: 0x040000D8 RID: 216
            SystemKernelDebuggerInformation,
            // Token: 0x040000D9 RID: 217
            SystemContextSwitchInformation,
            // Token: 0x040000DA RID: 218
            SystemRegistryQuotaInformation,
            // Token: 0x040000DB RID: 219
            SystemExtendServiceTableInformation,
            // Token: 0x040000DC RID: 220
            SystemPrioritySeperation,
            // Token: 0x040000DD RID: 221
            SystemVerifierAddDriverInformation,
            // Token: 0x040000DE RID: 222
            SystemVerifierRemoveDriverInformation,
            // Token: 0x040000DF RID: 223
            SystemProcessorIdleInformation,
            // Token: 0x040000E0 RID: 224
            SystemLegacyDriverInformation,
            // Token: 0x040000E1 RID: 225
            SystemCurrentTimeZoneInformation,
            // Token: 0x040000E2 RID: 226
            SystemLookasideInformation,
            // Token: 0x040000E3 RID: 227
            SystemTimeSlipNotification,
            // Token: 0x040000E4 RID: 228
            SystemSessionCreate,
            // Token: 0x040000E5 RID: 229
            SystemSessionDetach,
            // Token: 0x040000E6 RID: 230
            SystemSessionInformation,
            // Token: 0x040000E7 RID: 231
            SystemRangeStartInformation,
            // Token: 0x040000E8 RID: 232
            SystemVerifierInformation,
            // Token: 0x040000E9 RID: 233
            SystemVerifierThunkExtend,
            // Token: 0x040000EA RID: 234
            SystemSessionProcessInformation,
            // Token: 0x040000EB RID: 235
            SystemLoadGdiDriverInSystemSpace,
            // Token: 0x040000EC RID: 236
            SystemNumaProcessorMap,
            // Token: 0x040000ED RID: 237
            SystemPrefetcherInformation,
            // Token: 0x040000EE RID: 238
            SystemExtendedProcessInformation,
            // Token: 0x040000EF RID: 239
            SystemRecommendedSharedDataAlignment,
            // Token: 0x040000F0 RID: 240
            SystemComPlusPackage,
            // Token: 0x040000F1 RID: 241
            SystemNumaAvailableMemory,
            // Token: 0x040000F2 RID: 242
            SystemProcessorPowerInformation,
            // Token: 0x040000F3 RID: 243
            SystemEmulationBasicInformation,
            // Token: 0x040000F4 RID: 244
            SystemEmulationProcessorInformation,
            // Token: 0x040000F5 RID: 245
            SystemExtendedHandleInformation,
            // Token: 0x040000F6 RID: 246
            SystemLostDelayedWriteInformation,
            // Token: 0x040000F7 RID: 247
            SystemBigPoolInformation,
            // Token: 0x040000F8 RID: 248
            SystemSessionPoolTagInformation,
            // Token: 0x040000F9 RID: 249
            SystemSessionMappedViewInformation,
            // Token: 0x040000FA RID: 250
            SystemHotpatchInformation,
            // Token: 0x040000FB RID: 251
            SystemObjectSecurityMode,
            // Token: 0x040000FC RID: 252
            SystemWatchdogTimerHandler,
            // Token: 0x040000FD RID: 253
            SystemWatchdogTimerInformation,
            // Token: 0x040000FE RID: 254
            SystemLogicalProcessorInformation,
            // Token: 0x040000FF RID: 255
            SystemWow64SharedInformationObsolete,
            // Token: 0x04000100 RID: 256
            SystemRegisterFirmwareTableInformationHandler,
            // Token: 0x04000101 RID: 257
            SystemFirmwareTableInformation,
            // Token: 0x04000102 RID: 258
            SystemModuleInformationEx,
            // Token: 0x04000103 RID: 259
            SystemVerifierTriageInformation,
            // Token: 0x04000104 RID: 260
            SystemSuperfetchInformation,
            // Token: 0x04000105 RID: 261
            SystemMemoryListInformation,
            // Token: 0x04000106 RID: 262
            SystemFileCacheInformationEx,
            // Token: 0x04000107 RID: 263
            SystemThreadPriorityClientIdInformation,
            // Token: 0x04000108 RID: 264
            SystemProcessorIdleCycleTimeInformation,
            // Token: 0x04000109 RID: 265
            SystemVerifierCancellationInformation,
            // Token: 0x0400010A RID: 266
            SystemProcessorPowerInformationEx,
            // Token: 0x0400010B RID: 267
            SystemRefTraceInformation,
            // Token: 0x0400010C RID: 268
            SystemSpecialPoolInformation,
            // Token: 0x0400010D RID: 269
            SystemProcessIdInformation,
            // Token: 0x0400010E RID: 270
            SystemErrorPortInformation,
            // Token: 0x0400010F RID: 271
            SystemBootEnvironmentInformation,
            // Token: 0x04000110 RID: 272
            SystemHypervisorInformation,
            // Token: 0x04000111 RID: 273
            SystemVerifierInformationEx,
            // Token: 0x04000112 RID: 274
            SystemTimeZoneInformation,
            // Token: 0x04000113 RID: 275
            SystemImageFileExecutionOptionsInformation,
            // Token: 0x04000114 RID: 276
            SystemCoverageInformation,
            // Token: 0x04000115 RID: 277
            SystemPrefetchPatchInformation,
            // Token: 0x04000116 RID: 278
            SystemVerifierFaultsInformation,
            // Token: 0x04000117 RID: 279
            SystemSystemPartitionInformation,
            // Token: 0x04000118 RID: 280
            SystemSystemDiskInformation,
            // Token: 0x04000119 RID: 281
            SystemProcessorPerformanceDistribution,
            // Token: 0x0400011A RID: 282
            SystemNumaProximityNodeInformation,
            // Token: 0x0400011B RID: 283
            SystemDynamicTimeZoneInformation,
            // Token: 0x0400011C RID: 284
            SystemCodeIntegrityInformation,
            // Token: 0x0400011D RID: 285
            SystemProcessorMicrocodeUpdateInformation,
            // Token: 0x0400011E RID: 286
            SystemProcessorBrandString,
            // Token: 0x0400011F RID: 287
            SystemVirtualAddressInformation,
            // Token: 0x04000120 RID: 288
            SystemLogicalProcessorAndGroupInformation,
            // Token: 0x04000121 RID: 289
            SystemProcessorCycleTimeInformation,
            // Token: 0x04000122 RID: 290
            SystemStoreInformation,
            // Token: 0x04000123 RID: 291
            SystemRegistryAppendString,
            // Token: 0x04000124 RID: 292
            SystemAitSamplingValue,
            // Token: 0x04000125 RID: 293
            SystemVhdBootInformation,
            // Token: 0x04000126 RID: 294
            SystemCpuQuotaInformation,
            // Token: 0x04000127 RID: 295
            SystemNativeBasicInformation,
            // Token: 0x04000128 RID: 296
            SystemErrorPortTimeouts,
            // Token: 0x04000129 RID: 297
            SystemLowPriorityIoInformation,
            // Token: 0x0400012A RID: 298
            SystemBootEntropyInformation,
            // Token: 0x0400012B RID: 299
            SystemVerifierCountersInformation,
            // Token: 0x0400012C RID: 300
            SystemPagedPoolInformationEx,
            // Token: 0x0400012D RID: 301
            SystemSystemPtesInformationEx,
            // Token: 0x0400012E RID: 302
            SystemNodeDistanceInformation,
            // Token: 0x0400012F RID: 303
            SystemAcpiAuditInformation,
            // Token: 0x04000130 RID: 304
            SystemBasicPerformanceInformation,
            // Token: 0x04000131 RID: 305
            SystemQueryPerformanceCounterInformation,
            // Token: 0x04000132 RID: 306
            SystemSessionBigPoolInformation,
            // Token: 0x04000133 RID: 307
            SystemBootGraphicsInformation,
            // Token: 0x04000134 RID: 308
            SystemScrubPhysicalMemoryInformation,
            // Token: 0x04000135 RID: 309
            SystemBadPageInformation,
            // Token: 0x04000136 RID: 310
            SystemProcessorProfileControlArea,
            // Token: 0x04000137 RID: 311
            SystemCombinePhysicalMemoryInformation,
            // Token: 0x04000138 RID: 312
            SystemEntropyInterruptTimingInformation,
            // Token: 0x04000139 RID: 313
            SystemConsoleInformation,
            // Token: 0x0400013A RID: 314
            SystemPlatformBinaryInformation,
            // Token: 0x0400013B RID: 315
            SystemThrottleNotificationInformation,
            // Token: 0x0400013C RID: 316
            SystemHypervisorProcessorCountInformation,
            // Token: 0x0400013D RID: 317
            SystemDeviceDataInformation,
            // Token: 0x0400013E RID: 318
            SystemDeviceDataEnumerationInformation,
            // Token: 0x0400013F RID: 319
            SystemMemoryTopologyInformation,
            // Token: 0x04000140 RID: 320
            SystemMemoryChannelInformation,
            // Token: 0x04000141 RID: 321
            SystemBootLogoInformation,
            // Token: 0x04000142 RID: 322
            SystemProcessorPerformanceInformationEx,
            // Token: 0x04000143 RID: 323
            SystemSpare0,
            // Token: 0x04000144 RID: 324
            SystemSecureBootPolicyInformation,
            // Token: 0x04000145 RID: 325
            SystemPageFileInformationEx,
            // Token: 0x04000146 RID: 326
            SystemSecureBootInformation,
            // Token: 0x04000147 RID: 327
            SystemEntropyInterruptTimingRawInformation,
            // Token: 0x04000148 RID: 328
            SystemPortableWorkspaceEfiLauncherInformation,
            // Token: 0x04000149 RID: 329
            SystemFullProcessInformation,
            // Token: 0x0400014A RID: 330
            MaxSystemInfoClass
        }

        // Token: 0x02000029 RID: 41
        private enum SYSTEM_MEMORY_LIST_COMMAND
        {
            // Token: 0x0400014C RID: 332
            MemoryCaptureAccessedBits,
            // Token: 0x0400014D RID: 333
            MemoryCaptureAndResetAccessedBits,
            // Token: 0x0400014E RID: 334
            MemoryEmptyWorkingSets,
            // Token: 0x0400014F RID: 335
            MemoryFlushModifiedList,
            // Token: 0x04000150 RID: 336
            MemoryPurgeStandbyList,
            // Token: 0x04000151 RID: 337
            MemoryPurgeLowPriorityStandbyList,
            // Token: 0x04000152 RID: 338
            MemoryCommandMax
        }
    }
}
