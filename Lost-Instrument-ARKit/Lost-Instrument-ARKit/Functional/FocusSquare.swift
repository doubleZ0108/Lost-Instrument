//
//  FocusSquare.swift
//  Lost-Instrument-ARKit
//
//  Created by 齐旭晨 on 2020/6/13.
//  Copyright © 2020 齐旭晨. All rights reserved.
//

import SceneKit

class FocusSquare: SCNNode{
    
    var isClosed: Bool = true{
        didSet{
            geometry?.firstMaterial?.diffuse.contents = self.isClosed ? UIImage(named: "close") : UIImage(named: "open")
        }
    }
    
    override init() {
        super.init()
        
        let plane = SCNPlane(width: 0.1, height: 0.1)
        plane.firstMaterial?.diffuse.contents = UIImage(named: "close")
        plane.firstMaterial?.isDoubleSided = true
        
        geometry = plane
        eulerAngles.x = GLKMathDegreesToRadians(-90)
        
    }
    
    required init?(coder: NSCoder) {
        fatalError("init(coder:) has not been implemented")
    }
    
    func hide() {
        self.isHidden = true
        
        #if DEBUG
        print("focusSquare isHiden status: \(String(describing: self.isHidden))")
        #endif
    }
    
    func unhide() {
        self.isHidden = false
        
        #if DEBUG
        print("focusSquare isHiden status: \(String(describing: self.isHidden))")
        #endif
    }
}
