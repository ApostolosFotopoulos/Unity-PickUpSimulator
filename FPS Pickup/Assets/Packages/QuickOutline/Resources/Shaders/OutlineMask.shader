//
//  OutlineMask.shader
//  QuickOutline
//
//  Created by Chris Nolet on 2/21/18.
//  Copyright © 2018 Chris Nolet. All rights reserved.
//

Shader "Custom/Outline Mask" {
  Properties {
    [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Float) = 0
  }

  SubShader {
    Tags {
      "Queue" = "Transparent"
    }

    Pass {
      Name "Mask"
      Cull Off
      ZTest Off
      ZWrite on
      ColorMask 0
      Blend SrcAlpha OneMinusSrcAlpha

      Stencil {
        Ref 1
        Pass Replace
      }
    }
  }
}
