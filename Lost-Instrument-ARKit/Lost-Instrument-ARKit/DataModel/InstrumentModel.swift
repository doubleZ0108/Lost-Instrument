//
//  Enums.swift
//  Lost-Instrument-ARKit
//
//  Created by 齐旭晨 on 2020/7/9.
//  Copyright © 2020 齐旭晨. All rights reserved.
//

import Foundation

enum Instrument {
    case erhu, guzheng, pipa, yangqin
    
    var rootNodeName: String{
        switch self {
        case .erhu: return "erhu"
        case .guzheng: return "guzheng"
        case .pipa: return "pipa"
        case .yangqin: return "yangqin"
        }
    }
}
