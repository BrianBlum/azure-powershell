﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Commands.Compute.Common;
using Microsoft.Azure.Commands.Compute.Models;
using Microsoft.WindowsAzure.Commands.Utilities.Common;

namespace Microsoft.Azure.Commands.Compute
{
    [Cmdlet(
        VerbsCommon.Remove,
        ProfileNouns.DataDisk),
    OutputType(
        typeof(PSVirtualMachineProfile))]
    public class RemoveAzureVMDataDiskProfileCommand : AzurePSCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = HelpMessages.VMProfile)]
        [ValidateNotNullOrEmpty]
        public PSVirtualMachineProfile VMProfile { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 1,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = HelpMessages.VMDataDiskName)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        public override void ExecuteCmdlet()
        {
            if (this.VMProfile.StorageProfile == null)
            {
                this.VMProfile.StorageProfile = new StorageProfile();
            }

            if (this.VMProfile.StorageProfile.DataDisks == null)
            {
                this.VMProfile.StorageProfile.DataDisks = new List<DataDisk>();
            }

            var disks = this.VMProfile.StorageProfile.DataDisks.ToList();
            var comp = StringComparison.OrdinalIgnoreCase;
            disks.RemoveAll(d => string.Equals(d.Name, this.Name, comp));
            this.VMProfile.StorageProfile.DataDisks = disks;
            
            WriteObject(this.VMProfile);
        }
    }
}
