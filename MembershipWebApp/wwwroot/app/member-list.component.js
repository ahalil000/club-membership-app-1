System.register(["@angular/core","@angular/router","./member.service"],function(exports_1,context_1){"use strict";var core_1,router_1,member_service_1,MemberListComponent,__decorate=(context_1&&context_1.id,this&&this.__decorate||function(decorators,target,key,desc){var d,c=arguments.length,r=c<3?target:null===desc?desc=Object.getOwnPropertyDescriptor(target,key):desc;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)r=Reflect.decorate(decorators,target,key,desc);else for(var i=decorators.length-1;i>=0;i--)(d=decorators[i])&&(r=(c<3?d(r):c>3?d(target,key,r):d(target,key))||r);return c>3&&r&&Object.defineProperty(target,key,r),r}),__metadata=this&&this.__metadata||function(k,v){if("object"==typeof Reflect&&"function"==typeof Reflect.metadata)return Reflect.metadata(k,v)};return{setters:[function(core_1_1){core_1=core_1_1},function(router_1_1){router_1=router_1_1},function(member_service_1_1){member_service_1=member_service_1_1}],execute:function(){MemberListComponent=function(){function MemberListComponent(memberService,router){this.memberService=memberService,this.router=router}return MemberListComponent.prototype.ngOnInit=function(){var _this=this;console.log("MemberListComponent instantiated with the following type: "+this.class);var s=null;switch(this.class){case"current":default:this.title="Member List",s=this.memberService.getList()}s.subscribe(function(items){return _this.items=items},function(error){return _this.errorMessage=error}),this.columns=this.memberService.getColumns()},MemberListComponent.prototype.onSelect=function(item){this.selectedItem=item,console.log("Member "+this.selectedItem.ID+" has been clicked: loading item viewer..."),this.router.navigate(["member-detail-view",this.selectedItem.ID])},__decorate([core_1.Input(),__metadata("design:type",String)],MemberListComponent.prototype,"class",void 0),MemberListComponent=__decorate([core_1.Component({selector:"item-list",template:'\n    <div class="item-container">\n        <div class="panel panel-default">\n          <table class="table table-hover">\n          <tr>\n          <th class="item-list-header" *ngFor="let col of columns" >\n          {{ col }}\n          </th>\n          </tr>\n          <tr *ngFor="let item of items" (click)="onSelect(item)" >\n          <td class="item-list-cell" *ngFor="let col of columns" >\n          {{ item[col] }}\n          </td>\n          </tr>\n          </table>\n      </div>\n  </div>',styles:["\n        ul.items li { \n            cursor: pointer;\n        }\n        ul.items li.selected { \n            background-color: #dddddd; \n        }\n    "]}),__metadata("design:paramtypes",[member_service_1.MemberService,router_1.Router])],MemberListComponent)}(),exports_1("MemberListComponent",MemberListComponent)}}});
//# sourceMappingURL=member-list.component.js.map