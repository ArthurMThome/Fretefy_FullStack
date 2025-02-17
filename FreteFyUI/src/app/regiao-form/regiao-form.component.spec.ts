import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegiaoFormComponent } from './regiao-form.component';

describe('RegiaoFormComponent', () => {
  let component: RegiaoFormComponent;
  let fixture: ComponentFixture<RegiaoFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegiaoFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegiaoFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
