import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CidadeFormComponent } from './cidade-form.component';

describe('CidadeFormComponent', () => {
  let component: CidadeFormComponent;
  let fixture: ComponentFixture<CidadeFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CidadeFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CidadeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
