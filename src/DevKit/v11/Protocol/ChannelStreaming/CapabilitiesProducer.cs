﻿//----------------------------------------------------------------------- 
// ETP DevKit, 1.2
//
// Copyright 2019 Energistics
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//-----------------------------------------------------------------------

namespace Energistics.Etp.v11.Protocol.ChannelStreaming
{
    /// <summary>
    /// Provides common functionality for ChannelStreaming producer capabilities.
    /// </summary>
    public class CapabilitiesProducer : Etp11ProtocolCapabilities, ICapabilitiesProducer
    {
        /// <summary>
        /// Indicates the producer does not accept requests to stream individual channels but always sends all of its channels.
        /// </summary>
        public bool? SimpleStreamer
        {
            get { return TryGetValue<bool>("SimpleStreamer"); }
            set { Dictionary.SetValue<bool>("SimpleStreamer", value); }
        }
    }
}
